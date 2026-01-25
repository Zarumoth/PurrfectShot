using Microsoft.EntityFrameworkCore;
using PurrfectShot.Web.Data;
using PurrfectShot.Web.Models;
using PurrfectShot.Web.Services.Interfaces;
using PurrfectShot.Web.ViewModels.Photos;

namespace PurrfectShot.Web.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly PurrfectShotDbContext _dbContext;

        public PhotoService(PurrfectShotDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UploadPhotoAsync(PhotoUploadViewModel model, string wwwrootPath)
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
    }
}