using Microsoft.EntityFrameworkCore;
using PurrfectShot.Web.Data;
using PurrfectShot.Web.Models;
using PurrfectShot.Web.Services.Interfaces;
using PurrfectShot.Web.ViewModels.Calendar;
using PurrfectShot.Web.ViewModels.Cats;
using PurrfectShot.Web.ViewModels.Photos;
using PurrfectShot.Web.ViewModels.Votes;
using System.Globalization;

namespace PurrfectShot.Web.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly PurrfectShotDbContext _dbContext;

        public PhotoService(PurrfectShotDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UploadPhotoAsync(PhotoInputModel model, string wwwrootPath)
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
                FilePath = $"/images/uploads/{uniqueFileName}"
            };

            await _dbContext.Photos.AddAsync(photo);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PhotoDetailsViewModel> GetPhotoDetailsAsync(int photoId)
        {
            var photo = await _dbContext
                .Photos
                .Include(p => p.Cat)
                .Include(p => p.Votes)
                .SingleOrDefaultAsync(p => p.Id == photoId);

            if (photo == null)
            {
                return null;
            }

            return new PhotoDetailsViewModel
            {
                Id = photo.Id,
                ImageUrl = photo.FilePath,
                Caption = photo.Caption,
                UploadedOn = photo.DateUploaded.ToString("MMMM dd, yyyy"),
                CatId = photo.Cat.Id,
                CatName = photo.Cat.Name,
                CatBreed = photo.Cat.Breed,
                Rating = photo.Votes.Any() ? photo.Votes.Average(v => v.Stars) : 0.0,
                VotesCount = photo.Votes.Count
            };
        }

        public async Task SetProfilePicture(int photoId)
        {

            if (photoId <= 0)
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

        //Notes:
        //> We don't have a vote limit implemented (no user accounts)
        //> To think if we want to track vote timestamps (is this even useful?)
        public async Task VoteForPhotoAsync(VoteInputModel model)
        {
            var vote = new Vote
            {
                PhotoId = model.PhotoId,
                Stars = model.Stars,
                VoterName = model.VoterName,
            };

            await _dbContext.Votes.AddAsync(vote);
            await _dbContext.SaveChangesAsync();
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

            var calendarMonths = rawData
                .GroupBy(p => new { p.Year, p.Month })
                .Select(g => new CalendarMonthViewModel
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month),
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

        public async Task<PhotoEditInputModel?> GetPhotoForEditAsync(int photoId)
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

        public async Task<PhotoDeleteViewModel?> GetPhotoForDeleteAsync(int id)
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

        public async Task<int> DeletePhotoAsync(int id, string webRootPath)
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
    }
}