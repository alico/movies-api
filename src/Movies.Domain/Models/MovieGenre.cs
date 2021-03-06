namespace Movies.Domain.Models;
public class MovieGenre : BaseEntity<Guid>
{
    public Guid MovieId { get; set; }

    public Movie Movie { get; set; }

    public Guid GenreId { get; set; }

    public Genre Genre { get; set; }
}
