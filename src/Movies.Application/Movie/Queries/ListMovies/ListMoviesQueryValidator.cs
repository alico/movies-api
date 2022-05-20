using FluentValidation;


namespace Movies.Application.Movie.Queries;

public class ListMoviesQueryValidator : AbstractValidator<ListMoviesQuery>
{
    public ListMoviesQueryValidator()
    {
        RuleFor(v => v.PageNumber)
            .GreaterThan(0).WithMessage("must be greater than 0")
            .LessThan(1000).WithMessage("must be less than 1000");

        RuleFor(v => v.ItemCount)
            .GreaterThan(0).WithMessage("must be greater than 0")
            .LessThan(100).WithMessage("must be less than 100");

        RuleFor(f => f).Must(x => AtLeastOneFilterSelected(x.Title, x.YearOfRelease, x.Genres)).WithMessage("You must use at least one filter.");

    }

    public bool AtLeastOneFilterSelected(string title, int? yearOfRelease, IList<Guid> genres)
    {
        return !string.IsNullOrEmpty(title) || yearOfRelease.HasValue || genres?.Count > 0;
    }
}

