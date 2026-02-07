using PurrfectShot.Web.ViewModels.Cats;
using PurrfectShot.Web.ViewModels.Photos;

namespace PurrfectShot.Web.Services.Interfaces
{
    public interface ICatService
    {
        Task<IEnumerable<CatSelectViewModel>> GetAllCatsForSelectAsync();

        Task<int> AddCatAsync(CatInputModel model);

        Task<bool> ExistsByIdAsync(int id);

        Task<IEnumerable<CatCardViewModel>> GetFeaturedCatsAsync();

        Task<CatDetailsViewModel?> GetCatDetailsAsync(int id);

        Task<CatEditInputModel?> GetCatForEditAsync(int id);

        Task UpdateCatAsync(CatEditInputModel model);

        Task<CatDeleteViewModel> GetCatForDeleteAsync (int id);

        Task DeleteCatAsync(int id);
    }
}
