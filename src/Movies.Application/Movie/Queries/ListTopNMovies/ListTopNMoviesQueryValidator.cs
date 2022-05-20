using FluentValidation;


namespace Movies.Application.Movie.Queries;

public class ListTopNMoviesQueryValidator : AbstractValidator<ListTopNMoviesQuery>
{
    public ListTopNMoviesQueryValidator()
    {
        RuleFor(v => v.ItemCount)
             .GreaterThan(0).WithMessage("must be greater than 0")
             .LessThan(10).WithMessage("must be less than 10");
    }
}

