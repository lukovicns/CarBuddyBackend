using CarBuddy.Application;
using CarBuddy.Application.Models;
using CarBuddy.Application.Models.DTOs;
using CarBuddy.Application.Services;
using CarBuddy.Application.Validators;
using CarBuddy.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using CarBuddy.WebApi.Services;

namespace CarBuddy.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TripsController : ControllerBase
    {
        private readonly TripService _tripService;
        private readonly TripValidator _tripValidator;
        private readonly TripReservationValidator _tripReservationValidator;
        private readonly JwtTokenService _jwtTokenService;

        public TripsController(
            TripService tripService,
            TripValidator tripValidator,
            TripReservationValidator tripReservationValidator,
            JwtTokenService jwtTokenService)
        {
            _tripService = tripService;
            _tripValidator = tripValidator;
            _tripReservationValidator = tripReservationValidator;
            _jwtTokenService = jwtTokenService;
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetTripById(Guid id)
        {
            var trip = _tripService.GetTripById(id);

            return trip.IsEmpty
                ? Problem(Constants.TripNotFound, statusCode: 404)
                : Ok(new TripDto(trip));
        }

        [HttpGet("{userId:guid}/created-by-me")]
        public IActionResult GetTripsCreatedByMe(Guid userId, [FromQuery] Pagination pagination)
        {
            var tripsDto = _tripService.GetTripsCreatedByMe(userId)
                .Select(trip => new TripsDto(trip))
                .ToList();

            var result = new PagedResult<TripsDto>(tripsDto, pagination);

            return Ok(new
            {
                result.Content,
                result.Pagination,
            });
        }

        [HttpGet("{userId:guid}/reservations")]
        public IActionResult GetTripReservations(Guid userId, [FromQuery] Pagination pagination)
        {
            var tripsDto = _tripService.GetTripReservations(userId)
                .Select(trip => new TripsDto(trip));
            
            var result = new PagedResult<TripsDto>(tripsDto, pagination);

            return Ok(new
            {
                result.Content,
                result.Pagination,
            });
        }

        [HttpGet("{userId:guid}/history")]
        public IActionResult GetTripsHistory(Guid userId, [FromQuery] Pagination pagination)
        {
            var tripsDto = _tripService.GetTripsHistory(userId)
                .Select(trip => new TripsDto(trip));

            var result = new PagedResult<TripsDto>(tripsDto, pagination);

            return Ok(new
            {
                result.Content,
                result.Pagination,
            });
        }

        [HttpPost]
        public IActionResult CreateTrip([FromBody] Trip trip)
        {
            var validationResult = _tripValidator.Validate(trip);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(r => new { Error = r.ErrorMessage }));

            var result = _tripService.CreateTrip(trip);

            return result.HasError
                ? Problem(result.Message, statusCode: result.StatusCode)
                : Ok(result.Content);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateTrip(Guid id, [FromBody] Trip trip)
        {
            var foundTrip = _tripService.GetTripById(id);

            if (foundTrip.IsEmpty)
                return NotFound(Constants.TripNotFound);

            var validationResult = _tripValidator.Validate(trip);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(r => new { Error = r.ErrorMessage }));

            var result = _tripService.UpdateTrip(id, trip);

            return result.HasError
                ? Problem(result.Message, statusCode: result.StatusCode)
                : Ok(result.Content);
        }

        [HttpPut("{id:guid}/make-reservation")]
        public IActionResult MakeTripReservation(Guid id, [FromBody] TripReservation reservation)
        {
            var validationResult = _tripReservationValidator.Validate(reservation);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(r => new { Error = r.ErrorMessage }));

            var result = _tripService.MakeReservation(id, reservation);

            return result.HasError
                ? Problem(result.Message, statusCode: result.StatusCode)
                : Ok(result.Content);
        }
        
        [HttpPost]
        [Route("search")]
        public IActionResult SearchTrips([FromBody] SearchCriteria criteria, [FromQuery] Pagination pagination)
        {
            var userId = _jwtTokenService.GetUserId(Request);
            var trips = _tripService.SearchTrips(
                userId,
                criteria,
                pagination);

            return Ok(new
            {
                Content = trips.Content.Select(trip => new TripsDto(trip)),
                trips.Pagination,
            });
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteTrip(Guid id)
        {
            var result = _tripService.DeleteTrip(id);

            return result.HasError
                ? Problem(result.Message, statusCode: result.StatusCode)
                : Ok(result.Content);
        }
    }
}
