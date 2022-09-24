using CarBuddy.Application.Contracts.Repositories;
using CarBuddy.Application.Models;
using CarBuddy.Domain.Models;

namespace CarBuddy.Application.Services
{
    public class CarService
    {
        private readonly ICarRepository _carRepository;
        private readonly UserService _userService;

        public CarService(ICarRepository carRepository, UserService userService)
        {
            _carRepository = carRepository;
            _userService = userService;
        }

        public Result AddCar(Car car)
        {
            var driver = _userService.GetUserById(car.DriverId);

            if (driver.IsEmpty)
                return new Result(Constants.DriverNotFound, 404);

            var foundCar = _carRepository.GetCar(driver.Id);

            return !foundCar.IsEmpty
                ? new Result(Constants.UserAlreadyHasACar, 403)
                : new Result(_carRepository.AddCar(car, driver), 200);
        }
    }
}
