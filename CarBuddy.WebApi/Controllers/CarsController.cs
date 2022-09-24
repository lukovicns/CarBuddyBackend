using CarBuddy.Application.Models.DTOs;
using CarBuddy.Application.Validators;
using CarBuddy.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using CarBuddy.Application.Services;
using System.ComponentModel.DataAnnotations;

namespace CarBuddy.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly CarService _carService;
        private readonly CarValidator _carValidator;

        public CarsController(UserService userService,
            CarService carService,
            CarValidator carValidator)
        {
            _userService = userService;
            _carService = carService;
            _carValidator = carValidator;
        }

        [HttpGet]
        public IActionResult GetUserCar([FromQuery] Guid userId)
        {
            if (userId == Guid.Empty)
                return Ok(Enumerable.Empty<Car>());

            return Ok(_userService.GetUserCar(userId));
        }

        [HttpPost]
        public IActionResult AddCar([FromBody] Car car)
        {
            var validationResult = _carValidator.Validate(car);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(r => new { Error = r.ErrorMessage }));

            var result = _carService.AddCar(car);

            return result.HasError
                ? Problem(result.Message, statusCode: result.StatusCode)
                : Ok(new CarDto(result.Content as Car));
        }
    }
}
