using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Domain.Models;

[NotMapped]
public class MoviesAggregate
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int? YearOfRelease { get; set; }
    public int? RunningTime { get; set; }
    public double? RatingCount { get; set; }
    public double? TotalRatingScore { get; set; }
    public double? AverageRating { get; set; }

}

