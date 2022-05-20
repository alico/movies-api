using MediatR;
using Movies.Domain.Contracts;
using Movies.Domain.Models;

namespace Movies.Domain.Events
{
    public class RatingUpdatedEvent : IDomainEvent, INotification
    {
        public int PreviousScore { get; internal set; }

        public Rating Rate { get; internal set; }

        public RatingUpdatedEvent(Rating rate, int previousScore)
        {
            Rate = rate;
            PreviousScore = previousScore;
        }
    }
}
