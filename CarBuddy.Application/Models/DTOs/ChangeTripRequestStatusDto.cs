using CarBuddy.Application.Attributes;
using System;

namespace CarBuddy.Application.Models.DTOs
{
    public class ChangeTripRequestStatusDto
    {
        [GuidRequired]
        public Guid TripId { get; set; }

        [GuidRequired]
        public Guid PassengerId { get; set; }
    }
}
