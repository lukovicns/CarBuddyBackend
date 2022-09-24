using CarBuddy.Application.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace CarBuddy.Application.Models.DTOs
{
    public class SendTripRequestDto
    {
        [GuidRequired]
        public Guid TripId { get; set; }

        [GuidRequired]
        public Guid PassengerId { get; set; }

        [GuidRequired]
        public Guid DriverId { get; set; }

        [Required]
        public int NumberOfPassengers { get; set; }
    }
}
