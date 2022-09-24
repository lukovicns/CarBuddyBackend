using CarBuddy.Application.Contracts.Repositories;
using CarBuddy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarBuddy.Persistence.Repositories
{
    public class TripRequestRepository : ITripRequestRepository
    {
        private readonly CarBuddyContext _context;

        public TripRequestRepository(CarBuddyContext context) => _context = context;

        public IEnumerable<TripRequest> GetActiveTripRequests(Guid driverId)
        {
            return _context.TripRequests
                .Include(tr => tr.Trip)
                .Include(tr => tr.Passenger)
                .Where(tr => tr.Trip.DriverId == driverId && tr.Status == TripRequestStatus.Pending && tr.Trip.Date > DateTime.Now)
                .OrderByDescending(tr => tr.Trip.Date)
                .ThenBy(tr => tr.Trip.StartTime);
        }

        public IEnumerable<TripRequest> GetTripRequestsExcludePassenger(Guid tripId, Guid passengerId) =>
            _context.TripRequests.Where(tr => tr.TripId == tripId && tr.PassengerId != passengerId);

        public TripRequest GetTripRequest(Guid tripId, Guid passengerId)
        {
            return _context.TripRequests
                .Where(tr => tr.TripId == tripId && tr.PassengerId == passengerId)
                .FirstOrDefault();
        }

        public void SendTripRequest(Trip trip, int numberOfPassengers, User passenger)
        {
            _context.TripRequests.Add(new TripRequest(trip, numberOfPassengers, passenger));
            _context.SaveChanges();
        }

        public bool TripRequestExists(Guid tripId, Guid passengerId)
        {
            return _context.TripRequests
                .Where(tr => tr.TripId == tripId && tr.PassengerId == passengerId)
                .Any();
        }

        public void AcceptTripRequest(TripRequest tripRequest)
        {
            DeactivateAllExcept(tripRequest);
            _context.UserTrips.Add(new UserTrip(tripRequest.TripId, tripRequest.PassengerId));
            _context.Entry(tripRequest).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeclineTripRequest(TripRequest tripRequest)
        {
            _context.Entry(tripRequest).State = EntityState.Modified;
            _context.SaveChanges();
        }

        private void DeactivateAllExcept(TripRequest tripRequest)
        {
            var otherTripRequests = GetTripRequestsExcludePassenger(
                tripRequest.TripId,
                tripRequest.PassengerId);

            foreach (var other in otherTripRequests)
            {
                other.ChangeStatus(TripRequestStatus.Inactive);
                _context.Entry(other).State = EntityState.Modified;
            }
        }
    }
}
