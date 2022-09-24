using System;

namespace CarBuddy.Domain.Models
{
    public class TripRequest
    {
        public Guid PassengerId { get; private set; }
        public User Passenger { get; private set; }
        public Guid TripId { get; private set; }
        public Trip Trip { get; private set; }
        public int NumberOfPassengers { get; private set; }
        public TripRequestStatus Status { get; private set; }
        public bool IsEmpty => TripId == Empty.TripId;

        public static readonly TripRequest Empty = new TripRequest();

        private TripRequest() { }

        public TripRequest(Trip trip, int numberOfPassengers, User passenger)
        {
            Trip = trip;
            TripId = trip.Id;
            Passenger = passenger;
            PassengerId = passenger.Id;
            NumberOfPassengers = numberOfPassengers;
            Status = TripRequestStatus.Pending;
        }

        public bool Accept(Trip trip)
        {
            if (trip.HasAvailableSeats(NumberOfPassengers))
            {
                trip.NumberOfPassengers += NumberOfPassengers;
                ChangeStatus(TripRequestStatus.Accepted);
                return true;
            }

            ChangeStatus(TripRequestStatus.Inactive);
            return false;
        }

        public void Decline() => ChangeStatus(TripRequestStatus.Declined);

        public void ChangeStatus(TripRequestStatus status) =>
            Status = status;
    }
}
