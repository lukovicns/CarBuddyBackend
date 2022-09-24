using CarBuddy.Application.Contracts.Repositories;
using CarBuddy.Application.Models;
using CarBuddy.Application.Models.DTOs;
using CarBuddy.Domain.Extensions;
using CarBuddy.Domain.Models;
using System;
using System.Linq;
using System.Net;

namespace CarBuddy.Application.Services
{
    public class RatingService
    {
        private const int MaxDaysToRate = 7;

        private readonly IRatingRepository _ratingRepository;
        private readonly IUserTripRepository _userTripRepository;

        public RatingService(IRatingRepository ratingRepository,
            IUserTripRepository userTripRepository)
        {
            _ratingRepository = ratingRepository;
            _userTripRepository = userTripRepository;
        }

        public Result Rate(RatingDto data)
        {
            if (!CanRate(data.TripId, data.RecipientId, data.AuthorId))
                return new Result(new { Message = Constants.CannotRateDriver }, 400);

            var rating = new Rating(
                data.AuthorId,
                data.RecipientId,
                data.Evaluation,
                data.Comment);

            _ratingRepository.AddRating(rating);

            return new Result(new { rating.Evaluation }, 200);
        }

        public Result CanRateDriver(Guid tripId, Guid driverId, Guid passengerId)
        {
            bool canRate = CanRate(tripId, driverId, passengerId);
            var statusCode = canRate ? HttpStatusCode.OK : HttpStatusCode.Forbidden;
            return new Result(canRate, (int)statusCode);
        }

        private bool CanRate(Guid tripId, Guid driverId, Guid passengerId)
        {
            return _userTripRepository.GetUserTrips(tripId, driverId, passengerId)
                .Any(userTrip => IsDateValid(userTrip.Trip.Date));
        }

        private bool IsDateValid(DateTime tripDate)
        {
            var now = DateTime.Now;
            return now.IsGreaterThan(tripDate) && now.IsLessThan(tripDate.AddDays(MaxDaysToRate));
        }
    }
}
