using PurrfectShot.Web.ViewModels.Calendar;
using PurrfectShot.Web.ViewModels.Cats;
using PurrfectShot.Web.ViewModels.Photos;
using PurrfectShot.Web.ViewModels.Votes;

namespace PurrfectShot.Services.Data.Interfaces
{
    public interface IPhotoService
    {
        Task UploadPhotoAsync(PhotoInputModel model, string userId, string wwwrootPath);

        Task<PhotoDetailsViewModel> GetPhotoDetailsAsync(Guid photoId, string? userId);

        Task SetProfilePicture(Guid photoId);

        Task<(int totalPhotos, int totalVotes)> GetGlobalStatisticsAsync();

        Task<int> GetPhotoCountByCatIdAsync(int catId);

        Task VoteForPhotoAsync(VoteInputModel model, string userId, string userName);

        Task<bool> RemovePhotoVoteAsync(Guid photoId, string voterName);

        Task<IEnumerable<CalendarMonthViewModel>> GetCalendarMonthsAsync();

        Task<List<PhotoCardViewModel>> GetPhotosByMonthAsync(int year, int month);

        Task<PhotoEditInputModel?> GetPhotoForEditAsync(Guid photoId);

        Task UpdatePhotoAsync(PhotoEditInputModel model);

        Task<PhotoDeleteViewModel?> GetPhotoForDeleteAsync(Guid id);

        Task<int> DeletePhotoAsync(Guid id, string webRootPath);

        Task<bool> ToggleFavoriteAsync(Guid photoId, string userId);

        Task<IEnumerable<PhotoCardViewModel>> GetFavoritePhotosByUserIdAsync(string userId);

        Task<IEnumerable<PhotoCardViewModel>> GetPhotosByUserIdAsync(string userId);

        Task<IEnumerable<PhotoCardViewModel>> GetAllPhotosForAdminAsync();
    }
}
