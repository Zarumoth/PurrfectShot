using PurrfectShot.Web.ViewModels.Calendar;
using PurrfectShot.Web.ViewModels.Cats;
using PurrfectShot.Web.ViewModels.Photos;
using PurrfectShot.Web.ViewModels.Votes;

namespace PurrfectShot.Services.Data.Interfaces
{
    public interface IPhotoService
    {
        Task UploadPhotoAsync(PhotoInputModel model, string wwwrootPath);

        Task<PhotoDetailsViewModel> GetPhotoDetailsAsync(Guid photoId);

        Task SetProfilePicture(Guid photoId);

        Task<(int totalPhotos, int totalVotes)> GetGlobalStatisticsAsync();

        Task<int> GetPhotoCountByCatIdAsync(int catId);

        Task VoteForPhotoAsync(VoteInputModel model);

        Task<bool> RemovePhotoVoteAsync(Guid photoId, string voterName);

        Task<IEnumerable<CalendarMonthViewModel>> GetCalendarMonthsAsync();

        Task<List<PhotoCardViewModel>> GetPhotosByMonthAsync(int year, int month);

        Task<PhotoEditInputModel?> GetPhotoForEditAsync(Guid photoId);

        Task UpdatePhotoAsync(PhotoEditInputModel model);

        Task<PhotoDeleteViewModel?> GetPhotoForDeleteAsync(Guid id);

        Task<int> DeletePhotoAsync(Guid id, string webRootPath);
    }
}
