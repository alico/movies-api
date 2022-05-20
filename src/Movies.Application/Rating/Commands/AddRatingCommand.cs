using MediatR;

namespace Movies.Application.Rate.Commands;
public class AddRatingCommand : IRequest<Unit>
{
    public Guid UserId { get; set; }
    public Guid MovieId { get; set; }
    public int Score { get; set; }
}