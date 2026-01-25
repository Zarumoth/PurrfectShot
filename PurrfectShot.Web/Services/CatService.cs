using Microsoft.EntityFrameworkCore;
using PurrfectShot.Web.Data;
using PurrfectShot.Web.Services.Interfaces;
using PurrfectShot.Web.ViewModels.Cats;
using PurrfectShot.Web.Models;
using PurrfectShot.Web.ViewModels.Home;

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

        public async Task AddCatAsync(CatFormViewModel model)
        {
            var cat = new Cat
            {
                Name = model.Name,
                Breed = model.Breed,
                Description = model.Description
            };

            await _dbContext.Cats.AddAsync(cat);
            await _dbContext.SaveChangesAsync();
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
    }
}
