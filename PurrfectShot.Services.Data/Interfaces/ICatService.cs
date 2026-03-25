using PurrfectShot.Web.ViewModels.Cats;

namespace PurrfectShot.Services.Data.Interfaces
{
    public interface ICatService
    {
        Task<IEnumerable<CatSelectViewModel>> GetAllCatsForSelectAsync();

        Task<int> AddCatAsync(CatInputModel model, string userId);

        Task<bool> ExistsByIdAsync(int id);

        Task<(IEnumerable<CatCardViewModel> Cats, int TotalCount)> GetFeaturedCatsAsync(string? searchTerm, int currentPage, int pageSize);

        Task<CatDetailsViewModel?> GetCatDetailsAsync(int id);

        Task<CatEditInputModel?> GetCatForEditAsync(int id);

        Task UpdateCatAsync(CatEditInputModel model);

        Task<CatDeleteViewModel> GetCatForDeleteAsync (int id);

        Task DeleteCatPermanentlyAsync(int id);

        Task ArchiveCatAsync(int id);

        Task<IEnumerable<CatCardViewModel>> GetAllCatsForAdminAsync();

        Task RestoreArchivedCatAsync(int id);
    }
}
