using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PurrfectShot.Data;
using PurrfectShot.Data.Models;
using PurrfectShot.Services.Data.Interfaces;
using PurrfectShot.Web.ViewModels.Cats;

namespace PurrfectShot.Services.Data
{
    public class CatService(PurrfectShotDbContext dbContext, IMapper mapper, ILogger<CatService> logger) : ICatService
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
                .ProjectTo<CatSelectViewModel>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<int> AddCatAsync(CatInputModel model, string userId)
        {
            var cat = mapper.Map<Cat>(model);

            cat.OwnerId = userId;
            cat.IsActive = true;

            await dbContext.Cats.AddAsync(cat);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Cat created: {CatName} (Id: {CatId}) by User {UserId}", cat.Name, cat.Id, userId);

            return cat.Id;
        }

        public async Task<IEnumerable<CatCardViewModel>> GetFeaturedCatsAsync()
        {
            return await dbContext
                .Cats
                .AsNoTracking()
                .ProjectTo<CatCardViewModel>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<CatDetailsViewModel?> GetCatDetailsAsync(int id)
        {
            return await dbContext
                .Cats
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Where(c => c.Id == id)
                .ProjectTo<CatDetailsViewModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<CatEditInputModel?> GetCatForEditAsync(int id)
        {
            return await dbContext
                .Cats
                .IgnoreQueryFilters()
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

                logger.LogInformation("Cat updated: {CatId}", model.Id);
            }
        }

        public async Task<CatDeleteViewModel?> GetCatForDeleteAsync(int id)
        {
            return await dbContext
                .Cats
                .IgnoreQueryFilters()
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

            logger.LogCritical("PERMANENT DELETE: CatId {CatId} was removed from the database.", id);
        }

        public async Task ArchiveCatAsync(int id)
        {
            var cat = await dbContext.Cats.FindAsync(id);

            if (cat != null)
            {
                cat.IsActive = false;
                await dbContext.SaveChangesAsync();

                logger.LogInformation("Cat archived: {CatId}", id);
            }
        }

        public async Task<IEnumerable<CatCardViewModel>> GetAllCatsForAdminAsync()
        {
            return await dbContext
                .Cats
                .IgnoreQueryFilters()
                .AsNoTracking()
                .ProjectTo<CatCardViewModel>(mapper.ConfigurationProvider)
                .ToListAsync();

        }

        public async Task RestoreArchivedCatAsync(int id)
        {
            var cat = await dbContext
                .Cats
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cat != null)
            {
                cat.IsActive = true;
                await dbContext.SaveChangesAsync();
            }

            logger.LogInformation("Cat restored from archive: {CatId}", id);
        }
    }
}
