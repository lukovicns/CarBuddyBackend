using CarBuddy.Domain.Models;
using System;
using System.Collections.Generic;

namespace CarBuddy.Application.Contracts.Repositories
{
    public interface ITripRequestRepository
    {
        IEnumerable<TripRequest> GetActiveTripRequests(Guid driverId);

        IEnumerable<TripRequest> GetTripRequestsExcludePassenger(Guid tripId, Guid passengerId);

        TripRequest GetTripRequest(Guid tripId, Guid passengerId);

        void SendTripRequest(Trip trip, int numberOfPassengers, User passenger);

        bool TripRequestExists(Guid tripId, Guid passengerId);

        void AcceptTripRequest(TripRequest tripRequest);

        void DeclineTripRequest(TripRequest tripRequest);
    }
}
