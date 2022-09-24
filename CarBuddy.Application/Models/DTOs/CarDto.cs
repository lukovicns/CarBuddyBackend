using CarBuddy.Domain.Models;
using System;

namespace CarBuddy.Application.Models.DTOs
{
    public class CarDto
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Photo { get; set; }
        public int NumberOfSeats { get; set; }

        public CarDto(Car car)
        {
            Id = car.Id;
            Brand = car.Brand ?? "";
            Model = car.Model ?? "";
            Photo = car.Photo ?? "";
            NumberOfSeats = car.NumberOfSeats;
        }
    }
}
