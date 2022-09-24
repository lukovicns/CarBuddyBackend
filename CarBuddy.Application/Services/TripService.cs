using CarBuddy.Application.Contracts.Repositories;
using CarBuddy.Application.Models;
using CarBuddy.Domain.Models;
using System;
using System.Collections.Generic;

namespace CarBuddy.Application.Services
{
    public class TripService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserService _userService;

        public TripService(
            ITripRepository repository,
            IUserRepository userRepository,
            UserService userService)
        {
            _tripRepository = repository;
            _userRepository = userRepository;
            _userService = userService;
        }

        public Trip GetTripById(Guid id) => _tripRepository.GetTripById(id);

        public IEnumerable<Trip> GetTripsCreatedByMe(Guid userId) => _tripRepository.GetTripsCreatedByMe(userId);

        public IEnumerable<Trip> GetTripReservations(Guid userId) => _tripRepository.GetTripReservations(userId);

        public IEnumerable<Trip> GetTripsHistory(Guid userId) => _tripRepository.GetTripsHistory(userId);

        public Result CreateTrip(Trip trip)
        {
            var result = CheckIfDriverExists(trip);
            
            if (result.HasError)
                return result;
            
            var existingTrip = _tripRepository.TripExists(trip);

            if (!existingTrip.IsEmpty)
                return new Result(Constants.TripAlreadyExists, 403);

            var newTrip = _tripRepository.CreateTrip(trip);
            return new Result(new { newTrip.Id }, 200);
        }

        public PagedResult<Trip> SearchTrips(Guid userId, SearchCriteria criteria, Pagination pagination)
        {
            return _tripRepository.SearchTrips(
                userId,
                criteria,
                pagination.PageNumber,
                pagination.PageSize);
        }
        
        public Result UpdateTrip(Guid id, Trip trip)
        {
            if (trip.IsDeleted)
                return new Result(Constants.TripNotFound, 404);

            var result = CheckIfDriverExists(trip);

            if (result.HasError)
                return result;

            var updatedTrip = _tripRepository.UpdateTrip(id, trip);

            return new Result(updatedTrip, 200);
        }

        public Result DeleteTrip(Guid id)
        {
            var trip = _tripRepository.GetTripById(id);

            if (trip.IsEmpty)
                return new Result(Constants.TripNotFound, 404);

            _tripRepository.DeleteTrip(trip);
            return new Result(new { Message = Constants.TripDeletionSuccess }, 200);
        }

        private Result CheckIfDriverExists(Trip trip)
        {
            var driver = _userRepository.GetUserById(trip.DriverId);

            if (driver.IsEmpty)
                return new Result(Constants.DriverNotFound, 404);

            return !driver.IsDriver
                ? new Result(Constants.UserNotADriver, 403)
                : new Result(trip, 200);
        }

        public Result MakeReservation(Guid id, TripReservation reservation)
        {
            var user = _userService.GetUserById(reservation.UserId);

            if (user.IsEmpty)
                return new Result(Constants.UserNotFound, 404);

            var trip = GetTripById(id);

            if (trip.IsEmpty || trip.IsDeleted)
                return new Result(Constants.TripNotFound, 404);

            if (!trip.HasAvailableSeats(reservation.NumberOfPassengers))
                return new Result(Constants.NoPassengerSeatsLeft, 403);

            var updatedTrip = _tripRepository.MakeReservation(trip, reservation);
            return new Result(new { updatedTrip.Id }, 200);
        }
    }
}
