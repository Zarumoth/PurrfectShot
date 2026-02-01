using PurrfectShot.Web.ViewModels.Calendar;
using PurrfectShot.Web.ViewModels.Cats;
using PurrfectShot.Web.ViewModels.Photos;
using PurrfectShot.Web.ViewModels.Votes;

namespace PurrfectShot.Web.Services.Interfaces
{
    public interface IPhotoService
    {
        Task UploadPhotoAsync(PhotoUploadViewModel model, string wwwrootPath);

        Task<PhotoDetailsViewModel> GetPhotoDetailsAsync(int photoId);

        Task VoteForPhotoAsync(VoteViewModel model);

        Task<IEnumerable<CalendarMonthViewModel>> GetCalendarMonthsAsync();

        Task<List<PhotoByMonthViewModel>> GetPhotosByMonthAsync(int year, int month);

        Task<PhotoEditViewModel?> GetPhotoForEditAsync(int photoId);

        Task UpdatePhotoAsync(PhotoEditViewModel model);

        Task<PhotoDeleteViewModel?> GetPhotoForDeleteAsync(int id);

        Task<int> DeletePhotoAsync(int id, string webRootPath);
    }
}
