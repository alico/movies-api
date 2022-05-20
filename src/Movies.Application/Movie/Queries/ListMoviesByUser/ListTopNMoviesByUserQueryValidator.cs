using FluentValidation;


namespace Movies.Application.Movie.Queries;

public class ListTopNMoviesByUserQueryValidator : AbstractValidator<ListTopNMoviesByUserQuery>
{
    public ListTopNMoviesByUserQueryValidator()
    {
        RuleFor(v => v.UserId)
            .NotNull()
            .NotEmpty().WithMessage("is required.");

        RuleFor(v => v.ItemCount)
            .GreaterThan(0).WithMessage("must be greater than 0")
            .LessThan(10).WithMessage("must be less than 10");
    }
}

