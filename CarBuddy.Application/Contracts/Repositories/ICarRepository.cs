using CarBuddy.Domain.Models;
using System;

namespace CarBuddy.Application.Contracts.Repositories
{
    public interface ICarRepository
    {
        Car AddCar(Car car, User driver);
        Car GetCar(Guid driverId);
    }
}
