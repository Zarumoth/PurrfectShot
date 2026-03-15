using AutoMapper;
using PurrfectShot.Data.Models;
using PurrfectShot.Web.ViewModels.Cats;

namespace PurrfectShot.Web.Infrastructure.Profiles
{
    public class CatProfile : Profile
    {
        public CatProfile()
        {
            CreateMap<Cat, CatCardViewModel>()
                .ForMember(d => d.PhotoCount, opt => opt.MapFrom(s => s.Photos.Count))
                .ForMember(d => d.ProfileImageUrl, opt => opt.MapFrom(s =>
                    s.MainPhotoId.HasValue
                    ? s.MainPhoto.FilePath
                    : s.Photos.OrderByDescending(p => p.DateUploaded).Select(p => p.FilePath).FirstOrDefault()
                ));
        }
    }
}
