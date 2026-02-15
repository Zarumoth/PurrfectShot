using Microsoft.EntityFrameworkCore;
using PurrfectShot.Data;
using PurrfectShot.Services.Data.Interfaces;
using PurrfectShot.Web.ViewModels.Calendar;
using PurrfectShot.Data.Models;
using PurrfectShot.Web.ViewModels.Photos;
using PurrfectShot.Web.ViewModels.Votes;
using System.Globalization;

namespace PurrfectShot.Services.Data
{
    public class PhotoService : IPhotoService
    {
        private readonly PurrfectShotDbContext _dbContext;

        public PhotoService(PurrfectShotDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UploadPhotoAsync(PhotoInputModel model, string userId, string wwwrootPath)
        {
            //Generate unique file name + extension
            string fileExtension = Path.GetExtension(model.ImageFile.FileName);
            string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

            //Define the path to wwroot/images/uploads
            string uploadFolderPath = Path.Combine(wwwrootPath, "images", "uploads");

            //Check if the directory exists and create it if it doesn't
            if (!Directory.Exists(uploadFolderPath))
            {
                Directory.CreateDirectory(uploadFolderPath);
            }

            //Define the full path for the new file
            string fullFilePath = Path.Combine(uploadFolderPath, uniqueFileName);

            //Copy the file to the target location
            using (var fileStream = new FileStream(fullFilePath, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(fileStream);
            }

            //Create a new Photo entity and save it (the path) to the database
            var photo = new Photo
            {
                CatId = model.CatId,
                Caption = model.Caption,
                DateUploaded = DateTime.UtcNow,
                FilePath = $"/images/uploads/{uniqueFileName}",
                PublisherId = userId
            };

            await _dbContext.Photos.AddAsync(photo);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PhotoDetailsViewModel> GetPhotoDetailsAsync(Guid photoId, string? userId)
        {

            var photo = await _dbContext
                .Photos
                .Include(p => p.Cat)
                .Include(p => p.Votes)
                .Include(p => p.UserFavoritePhotos)
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == photoId);

            if (photo == null)
            {
                return null;
            }

            var bgCulture = new CultureInfo("bg-BG");

            var day = photo.DateUploaded.Day;
            var rawMonth = bgCulture.DateTimeFormat.GetMonthName(photo.DateUploaded.Month);
            var year = photo.DateUploaded.Year;

            string formattedDate = $"{day:D2} {ToTitleCase(rawMonth)} {year}";

            return new PhotoDetailsViewModel
            {
                Id = photo.Id,
                ImageUrl = photo.FilePath,
                Caption = photo.Caption,
                UploadedOn = formattedDate,
                CatId = photo.Cat.Id,
                CatName = photo.Cat.Name,
                CatBreed = photo.Cat.Breed,
                IsActive = photo.Cat.IsActive,
                IsMainPhoto = photo.Id == photo.Cat.MainPhotoId,
                Rating = photo.Votes.Any() ? photo.Votes.Average(v => v.Stars) : 0.0,
                VotesCount = photo.Votes.Count,
                Month = photo.DateUploaded.Month,
                Year = photo.DateUploaded.Year,
                MonthName = ToTitleCase(bgCulture.DateTimeFormat.GetMonthName(photo.DateUploaded.Month)),
                IsFavorite = userId != null && photo.UserFavoritePhotos.Any(fp => fp.UserId == userId)
            };
        }

        public async Task SetProfilePicture(Guid photoId)
        {

            if (photoId == Guid.Empty)
                throw new ArgumentException("Invalid photo ID.");

            var photo = await _dbContext
                .Photos
                .FindAsync(photoId);

            if (photo == null)
                throw new InvalidOperationException("Photo not found.");

            var cat = await _dbContext
                .Cats
                .FindAsync(photo.CatId);

            if (cat == null)
                throw new InvalidOperationException("Associated cat not found.");

            cat.MainPhotoId = photoId;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<(int totalPhotos, int totalVotes)> GetGlobalStatisticsAsync()
        {
            int totalPhotos = await _dbContext
                .Photos
                .CountAsync();

            int totalVotes = await _dbContext
                .Votes
                .CountAsync();

            return (totalPhotos, totalVotes);
        }

        public async Task<int> GetPhotoCountByCatIdAsync(int catId)
        {
            return await _dbContext
                .Photos
                .CountAsync(p => p.CatId == catId);
        }

        public async Task VoteForPhotoAsync(VoteInputModel model, string userId, string userName)
        {
            var existingVote = await _dbContext.Votes
                .FirstOrDefaultAsync(v => v.PhotoId == model.PhotoId && v.UserId == userId);

            if (existingVote != null)
            {
                existingVote.Stars = model.Stars;
                existingVote.VoterName = userName;
            }
            else
            {
                var newVote = new Vote
                {
                    PhotoId = model.PhotoId,
                    UserId = userId,
                    VoterName = userName,
                    Stars = model.Stars
                };

                await _dbContext.Votes.AddAsync(newVote);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> RemovePhotoVoteAsync(Guid photoId, string voterName)
        {
            var existingVote = await _dbContext.Votes
                .FirstOrDefaultAsync(v => v.PhotoId == photoId && v.VoterName == voterName);

            if (existingVote == null)
            {
                return false;
            }

            _dbContext.Votes.Remove(existingVote);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CalendarMonthViewModel>> GetCalendarMonthsAsync()
        {

            var rawData = await _dbContext
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

            var bgCulture = new CultureInfo("bg-BG");

            var calendarMonths = rawData
                .GroupBy(p => new { p.Year, p.Month })
                .Select(g => new CalendarMonthViewModel
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    MonthName = ToTitleCase(bgCulture.DateTimeFormat.GetMonthName(g.Key.Month)),
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
            return await _dbContext
                .Photos
                .AsNoTracking()
                .Where(p => p.DateUploaded.Year == year && p.DateUploaded.Month == month)
                .Select(p => new PhotoCardViewModel
                {
                    Id = p.Id,
                    FilePath = p.FilePath,
                    CatName = p.Cat.Name,
                    DateUploaded = p.DateUploaded,
                    Rating = p.Votes.Any() ? p.Votes.Average(v => v.Stars) : 0.0
                })
                .OrderByDescending(p => p.Rating)
                .ToListAsync();
        }

        public async Task<PhotoEditInputModel?> GetPhotoForEditAsync(Guid photoId)
        {
            return await _dbContext.Photos
                .AsNoTracking()
                .Where(p => p.Id == photoId)
                .Select(p => new PhotoEditInputModel
                {
                    Id = p.Id,
                    ImageUrl = p.FilePath,
                    Caption = p.Caption,
                    CatId = p.CatId,
                    CatName = p.Cat.Name

                })
                .FirstOrDefaultAsync();
        }

        public async Task UpdatePhotoAsync(PhotoEditInputModel model)
        {
            var photo = await _dbContext.Photos.FindAsync(model.Id);

            if (photo != null)
            {
                photo.Caption = model.Caption;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<PhotoDeleteViewModel?> GetPhotoForDeleteAsync(Guid id)
        {
            return await _dbContext.Photos
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new PhotoDeleteViewModel
                {
                    Id = p.Id,
                    CatId = p.CatId,
                    ImageUrl = p.FilePath,
                    CatName = p.Cat.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<int> DeletePhotoAsync(Guid id, string webRootPath)
        {
            var photo = await _dbContext.Photos.FindAsync(id);

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
            _dbContext.Photos.Remove(photo);
            await _dbContext.SaveChangesAsync();

            return catId;
        }

        public async Task<bool> ToggleFavoriteAsync(Guid photoId, string userId)
        {

            var favorite = await _dbContext.UserFavoritePhotos
                .FirstOrDefaultAsync(f => f.PhotoId == photoId && f.UserId == userId);

            if (favorite != null)
            {
                _dbContext.UserFavoritePhotos.Remove(favorite);
                await _dbContext.SaveChangesAsync();
                return false;
            }
            else
            {
                await _dbContext.UserFavoritePhotos.AddAsync(new UserFavoritePhoto
                {
                    PhotoId = photoId,
                    UserId = userId
                });
                await _dbContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<IEnumerable<PhotoCardViewModel>> GetFavoritePhotosByUserIdAsync(string userId)
        {
            return await _dbContext.UserFavoritePhotos
                .Where(f => f.UserId == userId)
                .Select(f => new PhotoCardViewModel
                {
                    Id = f.Photo.Id,
                    FilePath = f.Photo.FilePath,
                    CatName = f.Photo.Cat.Name,
                    DateUploaded = f.Photo.DateUploaded,
                    Rating = f.Photo.Votes.Any() ? f.Photo.Votes.Average(v => v.Stars) : 0.0
                })
                .OrderByDescending(p => p.DateUploaded)
                .ToListAsync();
        }

        public async Task<IEnumerable<PhotoCardViewModel>> GetPhotosByUserIdAsync(string userId)
        {
            return await _dbContext
                .Photos
                .AsNoTracking()
                .Where(p => p.PublisherId == userId)
                .Select(p => new PhotoCardViewModel
                {
                    Id = p.Id,
                    FilePath = p.FilePath,
                    CatName = p.Cat.Name,
                    DateUploaded = p.DateUploaded,
                    Rating = p.Votes.Any() ? p.Votes.Average(v => v.Stars) : 0.0
                })
                .OrderByDescending(p => p.DateUploaded)
                .ToListAsync();
        }

        //Helper: Used for BG-Month Upper Starting Letter
        private string ToTitleCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}