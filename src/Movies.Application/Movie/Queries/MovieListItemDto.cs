using AutoMapper;
using Movies.Application.Common.Mappings;

namespace Movies.Application.Movie.Queries;
public record MovieListItemDto : IMapFrom<Domain.Models.MoviesAggregate>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public int? YearOfRelease { get; set; }
    public int? RunningTime { get; set; }
    public double? AverageRating { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Models.MoviesAggregate, MovieListItemDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title))
            .ForMember(d => d.YearOfRelease, opt => opt.MapFrom(s => s.YearOfRelease))
            .ForMember(d => d.RunningTime, opt => opt.MapFrom(s => s.RunningTime))
            .ForMember(d => d.AverageRating, opt => opt.MapFrom(x => ((double?)(x.AverageRating.HasValue ? Math.Round(x.AverageRating.Value * 2, MidpointRounding.AwayFromZero) / 2 : null))));
    }
}