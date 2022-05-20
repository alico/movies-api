using System.ComponentModel.DataAnnotations;

namespace Movies.Domain.Models;
public class Movie : BaseEntity<Guid>
{
    [StringLength(200)]
    public string Title { get; set; }
    public int? YearOfRelease { get; set; }
    public int? RunningTime { get; set; }
    public double? RatingCount { get; set; }
    public double? TotalRatingScore { get; set; }
    public double? AverageRating { get { return TotalRatingScore / RatingCount; } private set { _ = TotalRatingScore / RatingCount; } }
    public IList<Rating> Ratings { get; set; }
    public IList<MovieGenre> Genres { get; set; }

    public Movie()
    {

    }
    public Movie(string title, int yearOfRelease, IList<Rating> ratings, int ratingCount, int totalRatingScore)
    {
        Title = title;
        YearOfRelease = yearOfRelease;
        Ratings = ratings;
        RatingCount = ratingCount;
        TotalRatingScore = totalRatingScore;
    }
}

