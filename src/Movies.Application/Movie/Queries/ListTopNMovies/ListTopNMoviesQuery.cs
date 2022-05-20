using MediatR;

namespace Movies.Application.Movie.Queries;
public class ListTopNMoviesQuery : IRequest<IEnumerable<MovieListItemDto>>
{
    public int ItemCount { get; set; }
}