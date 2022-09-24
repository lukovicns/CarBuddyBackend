using System;

namespace CarBuddy.Domain.Models
{
    public class Car : Entity
    {
        public Guid DriverId { get; set; }
        public User Driver { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Photo { get; set; }
        public int Year { get; set; }
        public int NumberOfSeats { get; set; }

        public static Car Empty = new Car();

        public bool IsEmpty => Id == Empty.Id;

        private Car() { }
    }
}
