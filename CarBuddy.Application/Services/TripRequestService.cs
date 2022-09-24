using CarBuddy.Application.Contracts.Repositories;
using CarBuddy.Application.Models;
using CarBuddy.Application.Models.DTOs;
using CarBuddy.Domain.Models;
using System;
using System.Linq;

namespace CarBuddy.Application.Services
{
    public class TripRequestService
    {
        private readonly TripService _tripService;
        private readonly UserService _userService;
        private readonly ITripRepository _tripRepository;
        private readonly ITripRequestRepository _tripRequestRepository;
        private readonly IUserRepository _userRepository;

        public TripRequestService(
            TripService tripService,
            UserService userService,
            ITripRepository tripRepository,
            ITripRequestRepository tripRequestRepository,
            IUserRepository userRepository)
        {
            _tripService = tripService;
            _userService = userService;
            _tripRepository = tripRepository;
            _tripRequestRepository = tripRequestRepository;
            _userRepository = userRepository;
        }

        public Result GetTripRequests(Guid driverId)
        {
            var driver = _userRepository.GetDriverById(driverId);

            if (driver == User.Empty)
                return new Result(Constants.DriverNotFound, 404);

            var tripRequests = _tripRequestRepository.GetActiveTripRequests(driver.Id)
                .Select(tripRequest => new TripRequestsDto(tripRequest));

            return new Result(tripRequests, 200);
        }

        public int GetTripRequestsCount(Guid driverId)
        {
            return _tripRequestRepository.GetActiveTripRequests(driverId)
                .Count();
        }

        public Result SendTripRequest(SendTripRequestDto tripRequest, Guid passengerId)
        {
            var tripRequestExists = _tripRequestRepository.TripRequestExists(tripRequest.TripId, passengerId);

            if (tripRequestExists)
                return new Result("Trip request already sent.", 400);

            var trip = _tripService.GetTripById(tripRequest.TripId);

            if (trip.IsEmpty)
                return new Result(Constants.TripNotFound, 404);

            if (!trip.IsActive)
                return new Result("Trip is no longer active.", 400);

            if (!trip.HasAvailableSeats(tripRequest.NumberOfPassengers))
                return new Result("This trip is fully booked.", 400);

            var passenger = _userService.GetUserById(passengerId);

            if (passenger.IsEmpty)
                return new Result(Constants.PassengerNotFound, 404);

            _tripRequestRepository.SendTripRequest(trip, tripRequest.NumberOfPassengers, passenger);

            return new Result(new { Message = Constants.TripRequestSentSuccessfully }, 200);
        }

        public bool TripRequestExists(Guid tripId, Guid passengerId) =>
            _tripRequestRepository.TripRequestExists(tripId, passengerId);

        public Result AcceptTripRequest(Guid tripId, Guid passengerId)
        {
            var trip = _tripRepository.GetTripById(tripId);
            var tripRequest = _tripRequestRepository.GetTripRequest(tripId, passengerId);
            bool accepted = tripRequest.Accept(trip);

            if (accepted)
            {
                _tripRequestRepository.AcceptTripRequest(tripRequest);
                return new Result(new { Message = Constants.TripRequestAccepted }, 200);
            }

            return DeclineTripRequest(tripRequest);
        }

        public Result DeclineTripRequest(Guid tripId, Guid passengerId)
        {
            var tripRequest = _tripRequestRepository.GetTripRequest(tripId, passengerId);
            return DeclineTripRequest(tripRequest);
        }

        private Result DeclineTripRequest(TripRequest tripRequest)
        {
            tripRequest.Decline();
            _tripRequestRepository.DeclineTripRequest(tripRequest);
            return new Result(new { Message = Constants.TripRequestDeclined }, 200);
        }
    }
}
