using MediatR;

namespace Movies.Application.Movie.Queries;
public class ListTopNMoviesByUserQuery : IRequest<IEnumerable<MovieListItemDto>>
{
    public Guid UserId { get; set; }
    public int ItemCount { get; set; }
}