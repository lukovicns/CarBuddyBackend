using CarBuddy.Application.Contracts.Repositories;
using CarBuddy.Application.Models;
using CarBuddy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarBuddy.Persistence.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly CarBuddyContext _context;

        public TripRepository(CarBuddyContext context) => _context = context;

        public Trip GetTripById(Guid id)
        {
            return GetTrips().FirstOrDefault(t => t.Id == id)
                   ?? Trip.Empty;
        }

        public IEnumerable<Trip> GetTripsCreatedByMe(Guid userId)
        {
            return GetTrips()
                .AsEnumerable()
                .Where(t => !t.IsExpired(DateTime.Now) && t.IsDriver(userId));
        }

        public IEnumerable<Trip> GetTripReservations(Guid userId)
        {
            return GetTrips()
                .Include(t => t.UserTrips)
                .AsEnumerable()
                .Where(t => t.IsActiveTrip(userId, DateTime.Now) && t.IsPassenger(userId));
        }

        public IEnumerable<Trip> GetTripsHistory(Guid userId)
        {
            return GetTrips()
                .Include(t => t.UserTrips)
                .AsEnumerable()
                .Where(t => t.IsPastTrip(userId, DateTime.Now) && t.IsPassenger(userId));
        }

        public Trip CreateTrip(Trip trip)
        {
            _context.Trips.Add(trip);
            _context.SaveChanges();
            return trip;
        }

        public PagedResult<Trip> SearchTrips(Guid userId, SearchCriteria criteria, int page, int size)
        {
            var trips = GetTrips()
                .AsEnumerable()
                .Where(trip => trip.DriverId != userId && trip.IsValid(
                    criteria.Date,
                    criteria.FromCity,
                    criteria.ToCity,
                    criteria.NumberOfPassengers));

            return new PagedResult<Trip>(trips, page, size);
        }

        public Trip TripExists(Trip trip)
        {
            return _context.Trips.AsEnumerable()
                       .SingleOrDefault(t => t.IsEqualTo(trip))
                   ?? Trip.Empty;
        }

        public Trip UpdateTrip(Guid id, Trip trip)
        {
            var foundTrip = _context.Trips.Single(t => t.Id == id);

            foundTrip.FromCity = trip.FromCity;
            foundTrip.ToCity = trip.ToCity;
            foundTrip.Date = trip.Date;
            foundTrip.StartTime = trip.StartTime;
            foundTrip.ArriveTime = trip.ArriveTime;
            foundTrip.Price = trip.Price;

            _context.Update(foundTrip);
            _context.SaveChanges();
            return foundTrip;
        }

        public void DeleteTrip(Trip trip)
        {
            trip.IsDeleted = true;
            _context.Update(trip);
            _context.SaveChanges();
        }

        public Trip MakeReservation(Trip trip, TripReservation reservation)
        {
            trip.NumberOfPassengers += reservation.NumberOfPassengers;
            trip.UserTrips.Add(new UserTrip(trip.Id, reservation.UserId));
            _context.Update(trip);
            _context.SaveChanges();
            return trip;
        }

        private IQueryable<Trip> GetTrips() => _context.Trips
            .Include(t => t.Driver)
            .ThenInclude(d => d.Car)
            .Include(t => t.Driver)
            .ThenInclude(t => t.RecipientRatings)
            .Include(ut => ut.UserTrips)
            .AsSplitQuery()
            .Where(t => !t.IsDeleted);
    }
}
