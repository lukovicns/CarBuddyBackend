using CarBuddy.Application.Models.DTOs;
using CarBuddy.Application.Services;
using CarBuddy.Hubs;
using CarBuddy.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;

namespace CarBuddy.WebApi.Controllers
{
    [Route("api/trip-requests")]
    [ApiController]
    public class TripRequestsController : ControllerBase
    {
        private readonly TripRequestService _tripRequestService;
        private readonly JwtTokenService _jwtTokenService;
        private readonly IHubContext<TripRequestHub, ITripRequestClient> _tripRequestHub;

        public TripRequestsController(
            TripRequestService tripRequestService,
            JwtTokenService jwtTokenService,
            IHubContext<TripRequestHub, ITripRequestClient> tripRequestHub)
        {
            _tripRequestService = tripRequestService;
            _jwtTokenService = jwtTokenService;
            _tripRequestHub = tripRequestHub;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetTripRequests()
        {
            var result = _tripRequestService.GetTripRequests(_jwtTokenService.GetUserId(Request));

            return result.HasError
                ? Problem(result.Message, statusCode: result.StatusCode)
                : Ok(result.Content);
        }

        [Authorize]
        [HttpPost("send-request")]
        public IActionResult SendTripRequest([FromBody] SendTripRequestDto tripRequest)
        {
            var result = _tripRequestService.SendTripRequest(tripRequest, _jwtTokenService.GetUserId(Request));

            if (result.HasError)
                return Problem(result.Message, statusCode: result.StatusCode);

            _tripRequestHub.Clients.All.SendTripRequest(tripRequest.DriverId);

            return Ok(result.Content);
        }

        [Authorize]
        [HttpGet("exists")]
        public IActionResult TripRequestExists([FromQuery] Guid tripId, [FromQuery] Guid passengerId)
        {
            bool exists = _tripRequestService.TripRequestExists(tripId, passengerId);
            return Ok(exists);
        }

        [Authorize]
        [HttpPost("accept")]
        public IActionResult AcceptTripRequest([FromBody] ChangeTripRequestStatusDto status)
        {
            var result = _tripRequestService.AcceptTripRequest(status.TripId, status.PassengerId);

            return result.HasError
                ? Problem(result.Message, statusCode: result.StatusCode)
                : Ok(result.Content);
        }

        [Authorize]
        [HttpPost("decline")]
        public IActionResult DeclineTripRequest([FromBody] ChangeTripRequestStatusDto status)
        {
            var result = _tripRequestService.DeclineTripRequest(status.TripId, status.PassengerId);

            return result.HasError
                ? Problem(result.Message, statusCode: result.StatusCode)
                : Ok(result.Content);
        }
    }
}
