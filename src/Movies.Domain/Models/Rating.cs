namespace Movies.Domain.Models;
public class Rating : BaseEntity<Guid>
{
    public Guid MovieId { get; set; }

    public Movie Movie { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    public int Score { get; set; }

    public Rating()
    {

    }

    public Rating(Guid movieId, Guid userId, int score)
    {
        MovieId = movieId;
        UserId = userId;
        Score = score;
    }
}