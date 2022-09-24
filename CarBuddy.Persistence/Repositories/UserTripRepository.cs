using CarBuddy.Application.Contracts.Repositories;
using CarBuddy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarBuddy.Persistence.Repositories
{
    public class UserTripRepository : IUserTripRepository
    {
        private readonly CarBuddyContext _context;

        public UserTripRepository(CarBuddyContext context) => _context = context;

        public IEnumerable<UserTrip> GetUserTrips(Guid tripId, Guid driverId, Guid passengerId)
        {
            return _context.UserTrips
                .Include(ut => ut.Trip)
                .AsEnumerable()
                .Where(ut => ut.TripId == tripId
                           && ut.Trip.DriverId == driverId
                           && ut.UserId == passengerId);
        }
    }
}
