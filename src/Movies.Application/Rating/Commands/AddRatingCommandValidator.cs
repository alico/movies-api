using FluentValidation;


namespace Movies.Application.Rate.Commands;

public class AddRatingCommandValidator : AbstractValidator<AddRatingCommand>
{
    public AddRatingCommandValidator()
    {
        RuleFor(v => v.UserId)
            .NotNull()
            .NotEmpty().WithMessage("is required.");

        RuleFor(v => v.MovieId)
           .NotNull()
           .NotEmpty().WithMessage("is required.");

        RuleFor(v => v.Score)
            .GreaterThan(0).WithMessage("must be greater than 0.")
            .LessThanOrEqualTo(5).WithMessage("must be less than or equal to 5.");
    }

    public bool AtLeastOneFilterSelected(string title, int? yearOfRelease, IList<Guid> genres)
    {
        return !string.IsNullOrEmpty(title) || yearOfRelease.HasValue || genres?.Count > 1;
    }
}

