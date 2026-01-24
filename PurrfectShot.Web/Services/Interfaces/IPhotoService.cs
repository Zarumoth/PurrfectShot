using PurrfectShot.Web.ViewModels.Photos;

namespace PurrfectShot.Web.Services.Interfaces
{
    public interface IPhotoService
    {
        Task UploadPhotoAsync(PhotoUploadViewModel model, string wwwrootPath);
    }
}
