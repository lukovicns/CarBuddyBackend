using System;

namespace CarBuddy.Domain.Models
{
    public class UserTrip
    {
        public Guid UserId { get; private set; }
        public Guid TripId { get; private set; }
        public User User { get; private set; }
        public Trip Trip { get; private set; }

        public UserTrip(Guid tripId, Guid userId)
        {
            TripId = tripId;
            UserId = userId;
        }
    }
}
