using CarBuddy.Domain.Models;
using System;
using System.Collections.Generic;

namespace CarBuddy.Application.Contracts.Repositories
{
    public interface IUserTripRepository
    {
        IEnumerable<UserTrip> GetUserTrips(Guid tripId, Guid driverId, Guid passengerId);
    }
}
