using PurrfectShot.Web.ViewModels.Calendar;
using PurrfectShot.Web.ViewModels.Cats;
using PurrfectShot.Web.ViewModels.Photos;
using PurrfectShot.Web.ViewModels.Votes;

namespace PurrfectShot.Web.Services.Interfaces
{
    public interface IPhotoService
    {
        Task UploadPhotoAsync(PhotoInputModel model, string wwwrootPath);

        Task<PhotoDetailsViewModel> GetPhotoDetailsAsync(int photoId);

        Task VoteForPhotoAsync(VoteInputModel model);

        Task<IEnumerable<CalendarMonthViewModel>> GetCalendarMonthsAsync();

        Task<List<PhotoCardViewModel>> GetPhotosByMonthAsync(int year, int month);

        Task<PhotoEditInputModel?> GetPhotoForEditAsync(int photoId);

        Task UpdatePhotoAsync(PhotoEditInputModel model);

        Task<PhotoDeleteViewModel?> GetPhotoForDeleteAsync(int id);

        Task<int> DeletePhotoAsync(int id, string webRootPath);
    }
}
