using System;

namespace CarBuddy.Domain.Models
{
    public class TripReservation
    {
        public Guid UserId { get; set; }
        public int NumberOfPassengers { get; set; }
    }
}
