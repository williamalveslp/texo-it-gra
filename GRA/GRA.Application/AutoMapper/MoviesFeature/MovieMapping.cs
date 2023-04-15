using AutoMapper;
using GRA.Application.ViewModels.MoviesFeature;
using GRA.Domain.Commands.MoviesFeature;

namespace GRA.Application.AutoMapper.MoviesFeature
{
    public class MovieMapping : Profile
    {
        public MovieMapping()
        {
            AllowNullDestinationValues = true;

            CreateMap<RegisterMovieViewModel, RegisterMovieCommand>(MemberList.None)
                .ForPath(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForPath(dest => dest.Producer, opt => opt.MapFrom(src => src.Producer))
                .ForPath(dest => dest.Studio, opt => opt.MapFrom(src => src.Studio))
                .ForPath(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForPath(dest => dest.IsWinner, opt => opt.MapFrom(src => src.IsWinner))
                .ReverseMap();

            CreateMap<Domain.Entities.MovieFeature.Movie, MovieViewModel>(MemberList.None)
                .ForPath(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForPath(dest => dest.Producer, opt => opt.MapFrom(src => src.Producer))
                .ForPath(dest => dest.Studio, opt => opt.MapFrom(src => src.Studio))
                .ForPath(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForPath(dest => dest.IsWinner, opt => opt.MapFrom(src => src.IsWinner))
                .ReverseMap();
        }
    }
}
