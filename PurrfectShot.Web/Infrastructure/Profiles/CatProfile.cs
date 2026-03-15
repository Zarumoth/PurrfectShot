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

            CreateMap<Cat, CatDeleteViewModel>()
                .ForMember(d => d.HasPhotos, opt => opt.MapFrom(s => s.Photos.Any()));

            CreateMap<Cat, CatDetailsViewModel>()
                .ForMember(d => d.Photos, opt => opt.MapFrom(s => s.Photos.OrderByDescending(p => p.Id)))
                .ForMember(d => d.OverallRating, opt => opt.MapFrom(s =>
                    s.Photos.SelectMany(p => p.Votes).Any()
                    ? s.Photos.SelectMany(p => p.Votes).Average(v => v.Stars)
                    : 0));

            CreateMap<Cat, CatEditInputModel>();

            CreateMap<CatEditInputModel, Cat>();

            CreateMap<CatInputModel, Cat>();

            CreateMap<Photo, CatPhotoViewModel>()
                .ForMember(d => d.ImageUrl, opt => opt.MapFrom(s => s.FilePath))
                .ForMember(d => d.Rating, opt => opt.MapFrom(s => s.Votes.Any() ? s.Votes.Average(v => v.Stars) : 0));
            
            CreateMap<Cat, CatSelectViewModel>();
        }
    }
}
