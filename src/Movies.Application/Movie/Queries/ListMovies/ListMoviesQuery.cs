using MediatR;

namespace Movies.Application.Movie.Queries;
public class ListMoviesQuery : IRequest<IEnumerable<MovieListItemDto>>
{
    public string Title { get; set; } = string.Empty;
    public int? YearOfRelease { get; set; }
    public IList<Guid>? Genres { get; set; }
    public int PageNumber { get; set; } = 1;
    public int ItemCount { get; set; } = 10;
}