using PurrfectShot.Web.ViewModels.Cats;
using PurrfectShot.Web.ViewModels.Home;

namespace PurrfectShot.Web.Services.Interfaces
{
    public interface ICatService
    {
        Task<IEnumerable<CatSelectViewModel>> GetAllCatsForSelectAsync();

        Task AddCatAsync(CatFormViewModel model);

        Task<IEnumerable<CatCardViewModel>> GetFeaturedCatsAsync();
    }
}
