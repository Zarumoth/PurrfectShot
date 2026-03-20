using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
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
    public class PhotoService(PurrfectShotDbContext dbContext, IMapper mapper) : IPhotoService
    {

        public async Task UploadPhotoAsync(PhotoInputModel model, string userId, string wwwrootPath)
        {
            //Generate unique file name + extension
            string fileExtension = Path.GetExtension(model.ImageFile.FileName).ToLower();
            if (!AllowedExtensions.Contains(fileExtension))
            {
                throw new InvalidOperationException("Невалиден формат на файла! Позволени са само .jpg, .jpeg и .png.");
            }
            var fileSize = model.ImageFile.Length;
            if (fileSize > MaxFileSize)
            {
                throw new InvalidOperationException("Снимката е твърде голяма. Максималният размер е 10MB.");
            }
            string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

            //Define the path to wwroot/images/uploads
            string uploadFolderPath = Path.Combine(wwwrootPath, "images", "uploads");

            //Check if the directory exists and create it if it doesn't
            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            //Define the full path for the new file
            string fullFilePath = Path.Combine(uploadFolderPath, uniqueFileName);

            //Copy the file to the target location
            using (var fileStream = new FileStream(fullFilePath, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(fileStream);
            }

            //Create a new Photo entity and save it (the path) to the database
            var photo = mapper.Map<Photo>(model);

            photo.Id = Guid.NewGuid();
            photo.DateUploaded = DateTime.UtcNow;
            photo.FilePath = $"/images/uploads/{uniqueFileName}";
            photo.PublisherId = userId;


            await dbContext.Photos.AddAsync(photo);
            await dbContext.SaveChangesAsync();
        }

        public async Task<PhotoDetailsViewModel> GetPhotoDetailsAsync(Guid photoId, string? userId)
        {

            var photo = await dbContext
                .Photos
                .Include(p => p.Cat)
                .Include(p => p.Votes)
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

            var cat = await dbContext
                .Cats
                .FindAsync(photo.CatId);

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
                .AsNoTracking()
                .Where(p => p.DateUploaded.Year == year && p.DateUploaded.Month == month)
                .ProjectTo<PhotoCardViewModel>(mapper.ConfigurationProvider)
                .OrderByDescending(p => p.Rating)
                .ToListAsync();
        }

        public async Task<PhotoEditInputModel?> GetPhotoForEditAsync(Guid photoId)
        {
            return await dbContext.Photos
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
                photo.Caption = model.Caption;
                await dbContext.SaveChangesAsync();
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

        public async Task<int> DeletePhotoAsync(Guid id, string webRootPath)
        {
            var photo = await dbContext.Photos.FindAsync(id);

            if (photo == null)
            {
                return 0;
            }

            int catId = photo.CatId;

            //Delete physical file
            string fullFilePath = Path.Combine(webRootPath, photo.FilePath.TrimStart('/'));

            if (File.Exists(fullFilePath))
            {
                File.Delete(fullFilePath);
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
                .Where(f => f.UserId == userId)
                .ProjectTo<PhotoCardViewModel>(mapper.ConfigurationProvider)
                .OrderByDescending(p => p.DateUploaded)
                .ToListAsync();
        }

        public async Task<IEnumerable<PhotoCardViewModel>> GetPhotosByUserIdAsync(string userId)
        {
            return await dbContext
                .Photos
                .AsNoTracking()
                .Where(p => p.PublisherId == userId)
                .ProjectTo<PhotoCardViewModel>(mapper.ConfigurationProvider)
                .OrderByDescending(p => p.DateUploaded)
                .ToListAsync();
        }
    }
}