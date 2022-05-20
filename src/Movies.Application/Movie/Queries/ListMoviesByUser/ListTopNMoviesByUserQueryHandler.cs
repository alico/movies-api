using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.Contracts;

namespace Movies.Application.Movie.Queries;
public class ListTopNMoviesByUserQueryHandler : IRequestHandler<ListTopNMoviesByUserQuery, IEnumerable<MovieListItemDto>>
{
    private readonly IQueryDbContext _context;
    private readonly IMapper _mapper;
    public ListTopNMoviesByUserQueryHandler(IQueryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<MovieListItemDto>> Handle(ListTopNMoviesByUserQuery request, CancellationToken cancellationToken)
    {

        return await _context.Ratings.Include(x => x.User).Include(x => x.Movie)
            .Where(x => x.UserId == request.UserId)
            .OrderByDescending(x => x.Score).ThenBy(x => x.Movie.Title)
            .Take(request.ItemCount)
            .Select(x => new Domain.Models.MoviesAggregate()
            {
                Id = x.Id,
                Title = x.Movie.Title,
                YearOfRelease = x.Movie.YearOfRelease,
                RunningTime = x.Movie.RunningTime,
                TotalRatingScore = x.Movie.TotalRatingScore,
                RatingCount = x.Movie.RatingCount,
                AverageRating = x.Movie.AverageRating,
            })
            .ProjectTo<MovieListItemDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}