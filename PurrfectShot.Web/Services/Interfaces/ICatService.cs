using PurrfectShot.Web.ViewModels.Cats;
using PurrfectShot.Web.ViewModels.Home;
using PurrfectShot.Web.ViewModels.Photos;

namespace PurrfectShot.Web.Services.Interfaces
{
    public interface ICatService
    {
        Task<IEnumerable<CatSelectViewModel>> GetAllCatsForSelectAsync();

        Task<int> AddCatAsync(CatFormViewModel model);

        Task<IEnumerable<CatCardViewModel>> GetFeaturedCatsAsync();

        Task<CatDetailsViewModel?> GetCatDetailsAsync(int id);

        Task<CatEditViewModel?> GetCatForEditAsync(int id);

        Task UpdateCatAsync(CatEditViewModel model);
    }
}
