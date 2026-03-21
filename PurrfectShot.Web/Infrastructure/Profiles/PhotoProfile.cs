using AutoMapper;
using PurrfectShot.Data.Models;
using PurrfectShot.Web.ViewModels.Photos;
using PurrfectShot.Web.ViewModels.Votes;
using static PurrfectShot.Common.DateFormatHelpers;
using System.Globalization;

namespace PurrfectShot.Web.Infrastructure.Profiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<Photo, PhotoCardViewModel>()
                .ForMember(d => d.Rating, opt => opt.MapFrom(s =>
                    s.Votes.Any() ? s.Votes.Average(v => v.Stars) : 0));

            CreateMap<Photo, PhotoDeleteViewModel>()
                .ForMember(d => d.ImageUrl, opt => opt.MapFrom(s => s.FilePath))
                .ForMember(d => d.CatName, opt => opt.MapFrom(s => s.Cat.Name));

            CreateMap<Photo, PhotoDetailsViewModel>()
                .ForMember(d => d.ImageUrl, opt => opt.MapFrom(s => s.FilePath))
                .ForMember(d => d.CatName, opt => opt.MapFrom(s => s.Cat.Name))
                .ForMember(d => d.CatBreed, opt => opt.MapFrom(s => s.Cat.Breed))
                .ForMember(d => d.IsActive, opt => opt.MapFrom(s => s.Cat.IsActive))
                .ForMember(d => d.IsMainPhoto, opt => opt.MapFrom(s => s.Id == s.Cat.MainPhotoId))
                .ForMember(d => d.PublisherName, opt => opt.MapFrom(s => s.Publisher.UserName))
                .ForMember(d => d.UploadedOn, opt => opt.MapFrom(s => s.DateUploaded.ToBulgarianDateString()))
                .ForMember(d => d.Rating, opt => opt.MapFrom(s => s.Votes.Any() ? s.Votes.Average(v => v.Stars) : 0))
                .ForMember(d => d.VotesCount, opt => opt.MapFrom(s => s.Votes.Count))
                .ForMember(d => d.Month, opt => opt.MapFrom(s => s.DateUploaded.Month))
                .ForMember(d => d.Year, opt => opt.MapFrom(s => s.DateUploaded.Year))
                .ForMember(d => d.MonthName, opt => opt.MapFrom(s => s.DateUploaded.Month.ToBulgarianMonthName()));

            CreateMap<Photo, PhotoEditInputModel>()
                .ForMember(d => d.ImageUrl, opt => opt.MapFrom(s => s.FilePath))
                .ForMember(d => d.CatName, opt => opt.MapFrom(s => s.Cat.Name));

            CreateMap<PhotoInputModel, Photo>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.DateUploaded, opt => opt.Ignore())
                .ForMember(d => d.FilePath, opt => opt.Ignore())
                .ForMember(d => d.PublisherId, opt => opt.Ignore())
                .ForMember(d => d.Votes, opt => opt.Ignore())
                .ForMember(d => d.UserFavoritePhotos, opt => opt.Ignore());

            CreateMap<PhotoInputModel, Photo>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.DateUploaded, opt => opt.Ignore())
                .ForMember(d => d.FilePath, opt => opt.Ignore())
                .ForMember(d => d.PublisherId, opt => opt.Ignore())
                .ForMember(d => d.Votes, opt => opt.Ignore())
                .ForMember(d => d.UserFavoritePhotos, opt => opt.Ignore());

            CreateMap<PhotoEditInputModel, Photo>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.FilePath, opt => opt.Ignore())
                .ForMember(d => d.CatId, opt => opt.Ignore());

            CreateMap<VoteInputModel, Vote>();
        }

        //private string ToTitleCase(string input)
        //{
        //    if (string.IsNullOrEmpty(input)) return input;
        //    return char.ToUpper(input[0]) + input.Substring(1);
        //}

        //private string FormatBulgarianDate(DateTime date)
        //{
        //    var bgCulture = new CultureInfo("bg-BG");
        //    string month = bgCulture.DateTimeFormat.GetMonthName(date.Month);
        //    month = char.ToUpper(month[0]) + month.Substring(1);
        //    return $"{date.Day:D2} {month} {date.Year}";
        //}

        //private string GetBulgarianMonth(int monthNumber)
        //{
        //    var bgCulture = new CultureInfo("bg-BG");
        //    string month = bgCulture.DateTimeFormat.GetMonthName(monthNumber);
        //    return char.ToUpper(month[0]) + month.Substring(1);
        //}
    }
}
