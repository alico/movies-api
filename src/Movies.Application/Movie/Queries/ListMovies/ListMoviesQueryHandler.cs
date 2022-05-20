using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Application.Services;
using Movies.Domain.Contracts;

namespace Movies.Application.Movie.Queries;
public class ListMoviesQueryHandler : IRequestHandler<ListMoviesQuery, IEnumerable<MovieListItemDto>>
{
    private readonly IQueryDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMovieListExpressionBuilderQueryBuilder _queryBuilder;
    public ListMoviesQueryHandler(IQueryDbContext context, IMapper mapper, IMovieListExpressionBuilderQueryBuilder queryBuilder)
    {
        _context = context;
        _mapper = mapper;
        _queryBuilder = queryBuilder;
    }
    public async Task<IEnumerable<MovieListItemDto>> Handle(ListMoviesQuery request, CancellationToken cancellationToken)
    {
        var expression = _queryBuilder.Init(request)
                        .AddTitle()
                        .AddGenres()
                        .AddYearOfRelease()
                        .Build();

        return await _context.Movies.Include(x => x.Genres)
            .Where(expression)
            .Skip((request.PageNumber - 1) * request.ItemCount)
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
            .OrderBy(x => x.Title)
            .ProjectTo<MovieListItemDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}