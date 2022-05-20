using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.Contracts;
using Movies.Domain.Events;

namespace Movies.Application.Movie.EventHandlers;
public class RatingCreatedEventHandler : INotificationHandler<RatingCreatedEvent>
{
    private readonly ICommandDBContext _context;


    public RatingCreatedEventHandler(ICommandDBContext context)
    {
        _context = context;
    }

    public async Task Handle(RatingCreatedEvent notification, CancellationToken cancellationToken)
    {
        if (notification?.Rate is not null)
        {
            var rate = notification.Rate;
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == rate.MovieId, cancellationToken);
            if (movie is not null)
            {
                movie.TotalRatingScore += rate.Score;
                movie.RatingCount += 1;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}