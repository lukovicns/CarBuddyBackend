using System;
using System.Collections.Generic;
using System.Linq;

namespace CarBuddy.Domain.Models
{
    public class User : Entity
    {
        public static User Empty => new User();

        public Guid? CarId { get; set; }
        public Car Car { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string Photo { get; set; }
        public bool IsActivated { get; set; }
        public string ActivationToken { get; set; }
        public DateTime CreatedAt { get; set; }
        public double AverageRating => GetAverageRating();

        public virtual ICollection<Trip> DriverTrips { get; set; }
        public virtual ICollection<TripRequest> TripRequests { get; set; }
        public virtual ICollection<UserTrip> UserTrips { get; set; }
        public virtual ICollection<Rating> AuthorRatings { get; set; }
        public virtual ICollection<Rating> RecipientRatings { get; set; }
        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
        public virtual ICollection<Conversation> FirstParticipantConversations { get; set; }
        public virtual ICollection<Conversation> SecondParticipantConversations { get; set; }

        private User() { }

        public User(
            string firstName,
            string lastName,
            string email,
            string password,
            int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Age = age;
        }

        public bool IsEmpty => Id == Empty.Id;
        public bool IsDriver => CarId != Guid.Empty && CarId != null;

        public void AddCar(Car car)
        {
            Car = car;
            CarId = car.Id;
        }

        private double GetAverageRating()
        {
            if (RecipientRatings is null || !RecipientRatings.Any())
                return default;

            return Math.Round(RecipientRatings.Select(r => r.Evaluation).Average(), 1, MidpointRounding.AwayFromZero);
        }
    }
}
