using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Configuration;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PurrfectShot.Common;
using PurrfectShot.Data;
using PurrfectShot.Data.Models;
using PurrfectShot.Services.Data.Interfaces;
using PurrfectShot.Web.ViewModels.Calendar;
using PurrfectShot.Web.ViewModels.Photos;
using PurrfectShot.Web.ViewModels.Votes;
using System.Globalization;
using static PurrfectShot.Common.DateFormatHelpers;
using static PurrfectShot.Web.Common.EntityValidation.Photo;

namespace PurrfectShot.Services.Data
{
    public class PhotoService(PurrfectShotDbContext dbContext, IMapper mapper, ILogger<PhotoService> logger, IConfiguration config) : IPhotoService
    {

        public async Task UploadPhotoAsync(PhotoInputModel model, string userId)
        {
            //Validation
            string fileExtension = Path.GetExtension(model.ImageFile.FileName).ToLower();

            if (!AllowedExtensions.Contains(fileExtension))
                throw new InvalidOperationException("Невалиден формат на файла! Позволени са само .jpg, .jpeg и .png.");

            if (model.ImageFile.Length > MaxFileSize)
                throw new InvalidOperationException("Снимката е твърде голяма. Максималният размер е 10MB.");

            //Cloudinary Setup
            Account account = new Account(
                config["Cloudinary:CloudName"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
            );

            Cloudinary cloudinary = new Cloudinary(account);

            string publicUrl = string.Empty;

            //Upload to Cloud
            using (var stream = model.ImageFile.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(model.ImageFile.FileName, stream),
                    Folder = "purrfect_shot", // Папка в твоя Cloudinary акаунт
                    Transformation = new Transformation().Quality("auto").FetchFormat("auto") // Оптимизация!
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                    throw new InvalidOperationException($"Грешка от облака: {uploadResult.Error.Message}");

                publicUrl = uploadResult.SecureUrl.AbsoluteUri;
            }

            //Register in DB
            var photo = mapper.Map<Photo>(model);
            photo.Id = Guid.NewGuid();
            photo.DateUploaded = DateTime.UtcNow;
            photo.FilePath = publicUrl; // Photo URL from Cloudinary
            photo.PublisherId = userId;

            await dbContext.Photos.AddAsync(photo);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("User {UserId} successfully uploaded photo {PhotoId} for Cat {CatId}", photo.PublisherId, photo.Id, photo.CatId);
        }

        //Old Upload Method - kept for reference, might be useful for reverting back to a local file storage approach if needed in the future.
        //Currently, we are using a cloud storage solution, so this method is not in use.
        //public async Task UploadPhotoAsync(PhotoInputModel model, string userId, string wwwrootPath)
        //{


        //    //Generate unique file name + extension
        //    string fileExtension = Path.GetExtension(model.ImageFile.FileName).ToLower();
        //    if (!AllowedExtensions.Contains(fileExtension))
        //    {
        //        throw new InvalidOperationException("Невалиден формат на файла! Позволени са само .jpg, .jpeg и .png.");
        //    }
        //    var fileSize = model.ImageFile.Length;
        //    if (fileSize > MaxFileSize)
        //    {
        //        throw new InvalidOperationException("Снимката е твърде голяма. Максималният размер е 10MB.");
        //    }
        //    string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

        //    //Define the path to wwroot/images/uploads
        //    string uploadFolderPath = Path.Combine(wwwrootPath, "images", "uploads");

        //    //Check if the directory exists and create it if it doesn't
        //    if (!Directory.Exists(uploadFolderPath))
        //        Directory.CreateDirectory(uploadFolderPath);

        //    //Define the full path for the new file
        //    string fullFilePath = Path.Combine(uploadFolderPath, uniqueFileName);

        //    //Copy the file to the target location
        //    using (var fileStream = new FileStream(fullFilePath, FileMode.Create))
        //    {
        //        await model.ImageFile.CopyToAsync(fileStream);
        //    }

        //    //Create a new Photo entity and save it (the path) to the database
        //    var photo = mapper.Map<Photo>(model);

        //    photo.Id = Guid.NewGuid();
        //    photo.DateUploaded = DateTime.UtcNow;
        //    photo.FilePath = $"/images/uploads/{uniqueFileName}";
        //    photo.PublisherId = userId;


        //    await dbContext.Photos.AddAsync(photo);
        //    await dbContext.SaveChangesAsync();

        //    logger.LogInformation("User {UserId} successfully uploaded photo {PhotoId} for Cat {CatId}", userId, photo.Id, photo.CatId);
        //}

        public async Task<PhotoDetailsViewModel?> GetPhotoDetailsAsync(Guid photoId, string? userId)
        {

            var photo = await dbContext
                .Photos
                .IgnoreQueryFilters()
                .Include(p => p.Cat)
                .Include(p => p.Votes)
                .Include(p => p.Publisher)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == photoId);

            if (photo == null) return null;

            var model = mapper.Map<PhotoDetailsViewModel>(photo);

            if (userId != null)
            {
                model.IsFavorite = await dbContext.UserFavoritePhotos
                    .AnyAsync(f => f.PhotoId == photoId && f.UserId == userId);
            }

            return model;
        }

        public async Task SetProfilePicture(Guid photoId)
        {

            if (photoId == Guid.Empty)
                throw new ArgumentException("Invalid photo ID.");

            var photo = await dbContext
                .Photos
                .FindAsync(photoId);

            if (photo == null)
                throw new InvalidOperationException("Photo not found.");

            var cat = await dbContext.Cats
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(c => c.Id == photo.CatId);

            if (cat == null)
                throw new InvalidOperationException("Associated cat not found.");

            cat.MainPhotoId = photoId;
            await dbContext.SaveChangesAsync();
        }

        public async Task<(int totalPhotos, int totalVotes)> GetGlobalStatisticsAsync()
        {
            int totalPhotos = await dbContext
                .Photos
                .CountAsync();

            int totalVotes = await dbContext
                .Votes
                .CountAsync();

            return (totalPhotos, totalVotes);
        }

        public async Task<int> GetPhotoCountByCatIdAsync(int catId)
        {
            return await dbContext
                .Photos
                .CountAsync(p => p.CatId == catId);
        }

        public async Task VoteForPhotoAsync(VoteInputModel model, string userId, string userName)
        {
            var existingVote = await dbContext.Votes
                .FirstOrDefaultAsync(v => v.PhotoId == model.PhotoId && v.UserId == userId);

            if (existingVote != null)
            {
                existingVote.Stars = model.Stars;
                existingVote.VoterName = userName;
            }
            else
            {
                var newVote = mapper.Map<Vote>(model);
                newVote.UserId = userId;
                newVote.VoterName = userName;

                await dbContext.Votes.AddAsync(newVote);
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> RemovePhotoVoteAsync(Guid photoId, string voterName)
        {
            var existingVote = await dbContext.Votes
                .FirstOrDefaultAsync(v => v.PhotoId == photoId && v.VoterName == voterName);

            if (existingVote == null)
            {
                return false;
            }

            dbContext.Votes.Remove(existingVote);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CalendarMonthViewModel>> GetCalendarMonthsAsync()
        {

            var rawData = await dbContext
                .Photos
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Select(p => new
                {
                    p.DateUploaded.Year,
                    p.DateUploaded.Month,
                    p.FilePath,
                    Rating = p.Votes.Any() ? p.Votes.Average(v => v.Stars) : 0
                })
                .ToListAsync();

            var calendarMonths = rawData
                .GroupBy(p => new { p.Year, p.Month })
                .Select(g => new CalendarMonthViewModel
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    MonthName = g.Key.Month.ToBulgarianMonthName(),
                    CoverImageUrl = g.OrderByDescending(x => x.Rating).First().FilePath,
                    PhotoCount = g.Count()
                })
                .OrderByDescending(m => m.Year)
                .ThenByDescending(m => m.Month)
                .ToList();

            return calendarMonths;
        }

        public async Task<List<PhotoCardViewModel>> GetPhotosByMonthAsync(int year, int month)
        {
            return await dbContext
                .Photos
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Where(p => p.DateUploaded.Year == year && p.DateUploaded.Month == month)
                .ProjectTo<PhotoCardViewModel>(mapper.ConfigurationProvider)
                .OrderByDescending(p => p.Rating)
                .ToListAsync();
        }

        public async Task<PhotoEditInputModel?> GetPhotoForEditAsync(Guid photoId)
        {
            return await dbContext.Photos
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Where(p => p.Id == photoId)
                .ProjectTo<PhotoEditInputModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task UpdatePhotoAsync(PhotoEditInputModel model)
        {
            var photo = await dbContext.Photos.FindAsync(model.Id);

            if (photo != null)
            {
                mapper.Map(model, photo);
                await dbContext.SaveChangesAsync();

                logger.LogInformation("Photo {PhotoId} was updated.", model.Id);
            }
        }

        public async Task<PhotoDeleteViewModel?> GetPhotoForDeleteAsync(Guid id)
        {
            return await dbContext.Photos
                .AsNoTracking()
                .Where(p => p.Id == id)
                .ProjectTo<PhotoDeleteViewModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<int> DeletePhotoAsync(Guid id)
        {
            var photo = await dbContext.Photos.FindAsync(id);

            if (photo == null)
                return 0;

            int catId = photo.CatId;

            //Delete from Cloud (Cloudinary)
            try
            {
                Account account = new Account(
                    config["Cloudinary:CloudName"],
                    config["Cloudinary:ApiKey"],
                    config["Cloudinary:ApiSecret"]
                );
                Cloudinary cloudinary = new Cloudinary(account);

                // Get File name from Cloudinary URL
                var uri = new Uri(photo.FilePath);
                var fileName = Path.GetFileNameWithoutExtension(uri.LocalPath);
                string publicId = $"purrfect_shot/{fileName}";

                await cloudinary.DestroyAsync(new DeletionParams(publicId));
                logger.LogInformation("The photo {PhotoId} was deleted", id);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Could not delete photo {PhotoId} from cloud.", id);
            }

            //Delete database record
            dbContext.Photos.Remove(photo);
            await dbContext.SaveChangesAsync();

            return catId;
        }

        public async Task<bool> ToggleFavoriteAsync(Guid photoId, string userId)
        {

            var favorite = await dbContext.UserFavoritePhotos
                .FirstOrDefaultAsync(f => f.PhotoId == photoId && f.UserId == userId);

            if (favorite != null)
            {
                dbContext.UserFavoritePhotos.Remove(favorite);
                await dbContext.SaveChangesAsync();
                return false;
            }
            else
            {
                await dbContext.UserFavoritePhotos.AddAsync(new UserFavoritePhoto
                {
                    PhotoId = photoId,
                    UserId = userId
                });
                await dbContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<IEnumerable<PhotoCardViewModel>> GetFavoritePhotosByUserIdAsync(string userId)
        {
            return await dbContext.UserFavoritePhotos
                .IgnoreQueryFilters()
                .Where(f => f.UserId == userId)
                .ProjectTo<PhotoCardViewModel>(mapper.ConfigurationProvider)
                .OrderByDescending(p => p.DateUploaded)
                .ToListAsync();
        }

        public async Task<IEnumerable<PhotoCardViewModel>> GetPhotosByUserIdAsync(string userId)
        {
            return await dbContext
                .Photos
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Where(p => p.PublisherId == userId)
                .ProjectTo<PhotoCardViewModel>(mapper.ConfigurationProvider)
                .OrderByDescending(p => p.DateUploaded)
                .ToListAsync();
        }

        public async Task<IEnumerable<PhotoCardViewModel>> GetAllPhotosForAdminAsync()
        {
            return await dbContext.Photos
                .IgnoreQueryFilters()
                .AsNoTracking()
                .OrderByDescending(p => p.DateUploaded)
                .ProjectTo<PhotoCardViewModel>(mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}