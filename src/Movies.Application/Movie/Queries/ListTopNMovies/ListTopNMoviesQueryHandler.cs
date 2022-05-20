using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.Contracts;

namespace Movies.Application.Movie.Queries;
public class ListTopNMoviesQueryHandler : IRequestHandler<ListTopNMoviesQuery, IEnumerable<MovieListItemDto>>
{
    private readonly IQueryDbContext _context;
    private readonly IMapper _mapper;
    public ListTopNMoviesQueryHandler(IQueryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<MovieListItemDto>> Handle(ListTopNMoviesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Movies
            .OrderByDescending(x => x.AverageRating).ThenBy(x => x.Title)
            .Take(request.ItemCount)
            .Select(x => new Domain.Models.MoviesAggregate()
            {
                Id = x.Id,
                Title = x.Title,
                YearOfRelease = x.YearOfRelease,
                RunningTime = x.RunningTime,
                TotalRatingScore = x.TotalRatingScore,
                RatingCount = x.RatingCount,
                AverageRating = x.AverageRating,
            })
            .ProjectTo<MovieListItemDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}