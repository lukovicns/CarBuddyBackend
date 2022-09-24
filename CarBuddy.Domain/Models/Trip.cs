using CarBuddy.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarBuddy.Domain.Models
{
    public class Trip : Entity
    {
        public static Trip Empty => new Trip();

        public Guid DriverId { get; set; }
        public User Driver { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan ArriveTime { get; set; }
        public double Price { get; set; }
        public int NumberOfPassengers { get; set; }
        public bool IsDeleted { get; set; }

        public int NumberOfAvailableSeats => GetNumberOfAvailableSeats();
        public bool IsEmpty => Id == Empty.Id;
        public bool IsActive => Date.AddTicks(StartTime.Ticks) > DateTime.Now;

        public virtual ICollection<UserTrip> UserTrips { get; set; }
        public virtual ICollection<TripRequest> TripRequests { get; set; }

        private Trip() { }

        public bool IsValid(DateTime date, string fromCity, string toCity, int numberOfPassengers)
        {
            return Date.EqualTo(date)
                && FromCity.EqualsIgnoreCase(fromCity)
                && ToCity.EqualsIgnoreCase(toCity)
                && NumberOfAvailableSeats >= numberOfPassengers;
        }

        public bool IsEqualTo(Trip trip)
        {
            return Id != trip.Id
                && DriverId == trip.DriverId
                && FromCity.EqualsIgnoreCase(trip.FromCity)
                && ToCity.EqualsIgnoreCase(trip.ToCity)
                && Date.EqualTo(trip.Date)
                && StartTime.IsEqualTo(trip.StartTime)
                && ArriveTime.IsEqualTo(trip.ArriveTime);
        }

        public bool IsActiveTrip(Guid userId, DateTime dateTime) => HasUserTrips(userId)
            && !IsExpired(dateTime);
        
        public bool IsPastTrip(Guid userId, DateTime dateTime) => HasUserTrips(userId)
            && IsExpired(dateTime);

        public bool IsDriver(Guid userId) => DriverId == userId;
        public bool IsPassenger(Guid userId) => DriverId != userId;
        
        public bool IsExpired(DateTime dateTime)
        {
            var tripDateTime = new DateTime(
                Date.Year,
                Date.Month,
                Date.Day,
                ArriveTime.Hours,
                ArriveTime.Minutes,
                ArriveTime.Seconds);

            return tripDateTime.IsLessThan(dateTime);
        }

        public bool HasAvailableSeats(int numberOfPassengers) => NumberOfAvailableSeats >= numberOfPassengers;

        private int GetNumberOfAvailableSeats() => Driver?.Car?.NumberOfSeats - NumberOfPassengers ?? 0;

        private bool HasUserTrips(Guid userId) => UserTrips.Select(ut => ut.UserId).Contains(userId);
    }
}
