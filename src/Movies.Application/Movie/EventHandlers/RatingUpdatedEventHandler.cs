using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.Contracts;
using Movies.Domain.Events;

namespace Movies.Application.Movie.EventHandlers;
public class RatingUpdatedEventHandler : INotificationHandler<RatingUpdatedEvent>
{
    private readonly ICommandDBContext _context;


    public RatingUpdatedEventHandler(ICommandDBContext context)
    {
        _context = context;
    }

    public async Task Handle(RatingUpdatedEvent notification, CancellationToken cancellationToken)
    {
        if (notification?.Rate is not null)
        {
            var rate = notification.Rate;
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == rate.MovieId, cancellationToken);
            if (movie is not null)
            {
                movie.TotalRatingScore += rate.Score - notification.PreviousScore;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}