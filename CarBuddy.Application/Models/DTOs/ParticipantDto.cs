using CarBuddy.Domain.Models;
using System;

namespace CarBuddy.Application.Models.DTOs
{
    public class ParticipantDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }

        public ParticipantDto(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Photo = user.Photo;
        }
    }
}
