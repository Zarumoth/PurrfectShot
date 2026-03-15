using Microsoft.EntityFrameworkCore;
using PurrfectShot.Data;
using PurrfectShot.Data.Models;
using PurrfectShot.Services.Data.Interfaces;
using PurrfectShot.Web.ViewModels.Cats;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace PurrfectShot.Services.Data
{
    public class CatService(PurrfectShotDbContext dbContext, IMapper mapper) : ICatService
    {

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await dbContext
                .Cats
                .AnyAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<CatSelectViewModel>> GetAllCatsForSelectAsync()
        {
            return await dbContext.Cats
                .AsNoTracking()
                .Where(c => c.IsActive)
                .ProjectTo<CatSelectViewModel>(mapper.ConfigurationProvider)
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

            await dbContext.Cats.AddAsync(cat);
            await dbContext.SaveChangesAsync();
            return cat.Id;
        }

        public async Task<IEnumerable<CatCardViewModel>> GetFeaturedCatsAsync()
        {
            return await dbContext
                .Cats
                .AsNoTracking()
                .Where(c => c.IsActive)
                .ProjectTo<CatCardViewModel>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<CatDetailsViewModel?> GetCatDetailsAsync(int id)
        {
            return await dbContext.Cats
                .AsNoTracking()
                .Where(c => c.Id == id)
                .ProjectTo<CatDetailsViewModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<CatEditInputModel?> GetCatForEditAsync(int id)
        {
            return await dbContext
                .Cats
                .AsNoTracking()
                .Where(c => c.Id == id)
                .ProjectTo<CatEditInputModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateCatAsync(CatEditInputModel model)
        {
            var cat = await dbContext.Cats.FindAsync(model.Id);

            if (cat != null)
            {
                mapper.Map(model, cat);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<CatDeleteViewModel?> GetCatForDeleteAsync(int id)
        {
            return await dbContext
                .Cats
                .AsNoTracking()
                .Where(c => c.Id == id)
                .ProjectTo<CatDeleteViewModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteCatPermanentlyAsync(int id)
        {
            var cat = new Cat { Id = id };
            dbContext.Cats.Remove(cat);
            await dbContext.SaveChangesAsync();
        }

        public async Task ArchiveCatAsync(int id)
        {
            var cat = await dbContext.Cats.FindAsync(id);

            if (cat != null)
            {
                cat.IsActive = false;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CatCardViewModel>> GetAllCatsForAdminAsync()
        {
            return await dbContext
                .Cats
                .AsNoTracking()
                .ProjectTo<CatCardViewModel>(mapper.ConfigurationProvider)
                .ToListAsync();

        }

        public async Task RestoreArchivedCatAsync(int id)
        {
            var cat = await dbContext
                .Cats
                .FindAsync(id);

            if (cat != null)
            {
                cat.IsActive = true;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
