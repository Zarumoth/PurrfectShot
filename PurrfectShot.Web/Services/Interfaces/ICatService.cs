using PurrfectShot.Web.ViewModels.Cats;

namespace PurrfectShot.Web.Services.Interfaces
{
    public interface ICatService
    {
        Task<IEnumerable<CatSelectViewModel>> GetAllCatsForSelectAsync();

        Task AddCatAsync(CatFormViewModel model);
    }
}
