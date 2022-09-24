using CarBuddy.Domain.Models;
using System;
using CarBuddy.Domain.Extensions;

namespace CarBuddy.Application.Models.DTOs
{
    public class TripsDto
    {
        public Guid Id { get; private set; }
        public Guid DriverId { get; private set; }
        public string DriverFirstName { get; private set; }
        public string DriverLastName { get; private set; }
        public string DriverPhoto { get; private set; }
        public double DriverRating { get; private set; }
        public string FromCity { get; private set; }
        public string ToCity { get; private set; }
        public string Date { get; private set; }
        public string StartTime { get; private set; }
        public string ArriveTime { get; private set; }
        public double Price { get; private set; }
        public int NumberOfAvailableSeats { get; private set; }
        public int NumberOfSeats { get; set; }

        public TripsDto(Trip trip)
        {
            Id = trip.Id;
            DriverId = trip.DriverId;
            DriverFirstName = trip.Driver.FirstName;
            DriverLastName = trip.Driver.LastName;
            DriverPhoto = trip.Driver.Photo;
            DriverRating = trip.Driver.AverageRating;
            Date = trip.Date.ToDateStringInvariantCulture();
            FromCity = trip.FromCity;
            ToCity = trip.ToCity;
            StartTime = trip.StartTime.ToHoursAndMinutesString();
            ArriveTime = trip.ArriveTime.ToHoursAndMinutesString();
            Price = trip.Price;
            NumberOfAvailableSeats = trip.NumberOfAvailableSeats;
            NumberOfSeats = trip.Driver.Car.NumberOfSeats;
        }
    }
}
