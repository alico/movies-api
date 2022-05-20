using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Application.Common.Exceptions;
using Movies.Domain.Contracts;
using Movies.Domain.Events;

namespace Movies.Application.Rate.Commands;
public class AddRatingCommandHandler : IRequestHandler<AddRatingCommand, Unit>
{
    private readonly IPublisher _publisher;
    private readonly ICommandDBContext _context;
    public AddRatingCommandHandler(IPublisher publisher, ICommandDBContext context)
    {
        _context = context;
        _publisher = publisher;
    }
    public async Task<Unit> Handle(AddRatingCommand request, CancellationToken cancellationToken)
    {
        if (!await _context.Users.AnyAsync(x => x.Id == request.UserId))
            throw new CustomException($"User {request.UserId} has not been found.");

        if (!await _context.Movies.AnyAsync(x => x.Id == request.MovieId))
            throw new CustomException($"Movie {request.MovieId} has not been found.");

        var rate = await _context.Ratings.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.MovieId == request.MovieId, cancellationToken);

        if (rate is null)
        {
            rate = new Domain.Models.Rating()
            {
                MovieId = request.MovieId,
                Score = request.Score,
                UserId = request.UserId,
            };
            await _context.Ratings.AddAsync(rate, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            await _publisher.Publish(new RatingCreatedEvent(rate), cancellationToken);
        }
        else
        {
            var previousScore = rate.Score;
            rate.Score = request.Score;
            await _context.SaveChangesAsync(cancellationToken);
            await _publisher.Publish(new RatingUpdatedEvent(rate, previousScore), cancellationToken);
        }

        return Unit.Value;
    }
}