using CarBuddy.Domain.Models;

namespace CarBuddy.Application.Models.DTOs
{
    public class UserDto
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Age { get; private set; }
        public string Photo { get; private set; }
        public double Rating { get; private set; }
        
        public UserDto(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Age = user.Age;
            Photo = user.Photo;
            Rating = user.AverageRating;
        }
    }
}
