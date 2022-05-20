using MediatR;
using Movies.Domain.Contracts;
using Movies.Domain.Models;

namespace Movies.Domain.Events
{
    public class RatingCreatedEvent : IDomainEvent, INotification
    {
        public Rating Rate { get; internal set; }

        public RatingCreatedEvent(Rating rate)
        {
            Rate = rate;
        }
    }
}
