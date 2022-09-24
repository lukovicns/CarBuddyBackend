using CarBuddy.Application.Models.DTOs;
using CarBuddy.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarBuddy.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RatingController : ControllerBase
    {
        private readonly RatingService _ratingService;

        public RatingController(RatingService ratingService) => _ratingService = ratingService;

        [Authorize]
        [HttpPost("rate")]
        public IActionResult Rate([FromBody] RatingDto rating)
        {
            var result = _ratingService.Rate(rating);

            return result.HasError
                ? Problem(result.Message, statusCode: result.StatusCode)
                : Ok(result.Content);
        }

        [Authorize]
        [HttpGet("canRate")]
        public IActionResult CanRateDriver(
            [FromQuery] Guid tripId,
            [FromQuery] Guid driverId,
            [FromQuery] Guid passengerId)
        {
            var result = _ratingService.CanRateDriver(tripId, driverId, passengerId);

            return result.HasError
                ? Ok(false)
                : Ok(true);
        }
    }
}
