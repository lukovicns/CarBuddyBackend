using CarBuddy.Domain.Extensions;
using CarBuddy.Domain.Models;

namespace CarBuddy.Application.Models.DTOs
{
    public class TripDto : TripsDto
    {
        public string CarBrand { get; private set; }
        public string CarModel { get; private set; }
        public string DriverCreatedAt { get; private set; }

        public TripDto(Trip trip) : base(trip)
        {
            CarBrand = trip.Driver.Car.Brand;
            CarModel = trip.Driver.Car.Model;
            NumberOfSeats = trip.Driver.Car.NumberOfSeats;
            DriverCreatedAt = trip.Driver.CreatedAt.ToDateStringInvariantCulture();
        }
    }
}
