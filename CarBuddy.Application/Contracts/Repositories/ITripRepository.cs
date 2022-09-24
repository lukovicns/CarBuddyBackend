using CarBuddy.Application.Models;
using CarBuddy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarBuddy.Application.Contracts.Repositories
{
    public interface ITripRepository
    {
        Trip GetTripById(Guid id);

        IEnumerable<Trip> GetTripsCreatedByMe(Guid userId);

        IEnumerable<Trip> GetTripReservations(Guid userId);

        IEnumerable<Trip> GetTripsHistory(Guid userId);

        PagedResult<Trip> SearchTrips(Guid userId, SearchCriteria criteria, int page, int size);

        Trip CreateTrip(Trip trip);

        Trip TripExists(Trip trip);

        Trip UpdateTrip(Guid id, Trip trip);

        void DeleteTrip(Trip trip);

        Trip MakeReservation(Trip trip, TripReservation reservation);
    }
}
