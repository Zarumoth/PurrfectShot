using Microsoft.EntityFrameworkCore;
using PurrfectShot.Web.Data;
using PurrfectShot.Web.Models;
using PurrfectShot.Web.Services.Interfaces;
using PurrfectShot.Web.ViewModels.Cats;
using PurrfectShot.Web.ViewModels.Photos;

namespace PurrfectShot.Web.Services
{
    public class CatService : ICatService
    {
        private readonly PurrfectShotDbContext _dbContext;

        public CatService(PurrfectShotDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CatSelectViewModel>> GetAllCatsForSelectAsync()
        {
            return await _dbContext.Cats
                .AsNoTracking()
                .Select(c => new CatSelectViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<int> AddCatAsync(CatInputModel model)
        {
            var cat = new Cat
            {
                Name = model.Name,
                Breed = model.Breed,
                Description = model.Description
            };

            await _dbContext.Cats.AddAsync(cat);
            await _dbContext.SaveChangesAsync();
            return cat.Id;
        }

        public async Task<IEnumerable<CatCardViewModel>> GetFeaturedCatsAsync()
        {
            return await _dbContext
                .Cats
                .AsNoTracking()
                .Select(c => new CatCardViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Breed = c.Breed,
                    ProfileImageUrl = c.Photos
                        .OrderByDescending(p => p.DateUploaded)
                        .Select(p => p.FilePath)
                        .FirstOrDefault()
                })
                .ToListAsync();
        }

        public async Task<CatDetailsViewModel?> GetCatDetailsAsync(int id)
        {
            return await _dbContext
                .Cats
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new CatDetailsViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Breed = c.Breed,
                    Description = c.Description,
                    Photos = c.Photos
                        .Select(p => new CatPhotoViewModel
                        {
                            Id = p.Id,
                            ImageUrl = p.FilePath,
                            Rating = p.Votes.Any()
                                ? p.Votes.Average(v => v.Stars)
                                : 0,
                            DateUploaded = p.DateUploaded
                        })
                        .OrderByDescending(p => p.Id)
                        .ToList(),
                    OverallRating = c.Photos.SelectMany(p => p.Votes).Any()
                        ? c.Photos.SelectMany(p => p.Votes).Average(v => v.Stars)
                        : 0
                })
                .FirstOrDefaultAsync();
        }

        //GET
        public async Task<CatEditInputModel?> GetCatForEditAsync(int id)
        {
            return await _dbContext.Cats
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new CatEditInputModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Breed = c.Breed,
                    Description = c.Description
                })
                .FirstOrDefaultAsync();
        }

        //POST
        public async Task UpdateCatAsync(CatEditInputModel model)
        {
            var cat = await _dbContext.Cats.FindAsync(model.Id);

            if (cat != null)
            {
                cat.Name = model.Name;
                cat.Breed = model.Breed;
                cat.Description = model.Description;

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<CatDeleteViewModel?> GetCatForDeleteAsync(int id)
        {
            return await _dbContext.Cats
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new CatDeleteViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .FirstOrDefaultAsync();
        }

        public async Task DeleteCatAsync(int id)
        {
            var cat = new Cat { Id = id };
            _dbContext.Cats.Remove(cat);
            await _dbContext.SaveChangesAsync();
        }
    }
}
