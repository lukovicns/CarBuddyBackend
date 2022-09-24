using CarBuddy.Application.Contracts.Repositories;
using CarBuddy.Domain.Models;
using System;
using System.Linq;

namespace CarBuddy.Persistence.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarBuddyContext _context;

        public CarRepository(CarBuddyContext context) => _context = context;

        public Car AddCar(Car car, User driver)
        {
            car.Id = Guid.NewGuid();
            driver.AddCar(car);
            _context.Users.Update(driver);
            _context.Cars.Add(car);
            _context.SaveChanges();
            return car;
        }

        public Car GetCar(Guid driverId)
        {
            return _context.Cars.FirstOrDefault(c => c.DriverId == driverId)
                   ?? Car.Empty;
        }
    }
}
