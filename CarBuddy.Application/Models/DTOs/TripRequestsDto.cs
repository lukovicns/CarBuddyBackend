using CarBuddy.Domain.Extensions;
using CarBuddy.Domain.Models;
using System;

namespace CarBuddy.Application.Models.DTOs
{
    public class TripRequestsDto
    {
        public Guid TripId { get; set; }
        public Guid PassengerId { get; set; }
        public string PassengerName { get; set; }
        public string PassengerPhoto { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public int NumberOfPassengers { get; set; }

        public TripRequestsDto(TripRequest tripRequest)
        {
            TripId = tripRequest.TripId;
            PassengerId = tripRequest.PassengerId;
            PassengerName = tripRequest.Passenger.FirstName;
            PassengerPhoto = tripRequest.Passenger.Photo;
            FromCity = tripRequest.Trip.FromCity;
            ToCity = tripRequest.Trip.ToCity;
            Date = tripRequest.Trip.Date.ToDateStringInvariantCulture();
            StartTime = tripRequest.Trip.StartTime.ToHoursAndMinutesString();
            NumberOfPassengers = tripRequest.NumberOfPassengers;
        }
    }
}
