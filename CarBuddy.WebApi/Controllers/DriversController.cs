using CarBuddy.Application;
using CarBuddy.Application.Models.DTOs;
using CarBuddy.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarBuddy.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriversController : ControllerBase
    {
        private readonly UserService _userService;

        public DriversController(UserService userService) => _userService = userService;

        [HttpGet("{driverId:guid}")]
        public IActionResult GetDriverById(Guid driverId)
        {
            var driver = _userService.GetDriverById(driverId);

            return driver.IsEmpty
                ? Problem(Constants.DriverNotFound, statusCode: 404)
                : Ok(new DriverDto(driver));
        }
    }
}
