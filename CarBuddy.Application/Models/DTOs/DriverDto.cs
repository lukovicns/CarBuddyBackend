using CarBuddy.Domain.Models;

namespace CarBuddy.Application.Models.DTOs
{
    public class DriverDto : UserDto
    {
        public CarDto Car { get; private set; }
        public int NumberOfDrivings { get; private set; }

        public DriverDto(User user) : base(user)
        {
            Car = new CarDto(user.Car);
            NumberOfDrivings = user.UserTrips?.Count ?? 0;
        }
    }
}
