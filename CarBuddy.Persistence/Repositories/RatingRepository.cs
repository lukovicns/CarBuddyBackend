using CarBuddy.Application.Contracts.Repositories;
using CarBuddy.Domain.Models;
using System;
using System.Linq;

namespace CarBuddy.Persistence.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly CarBuddyContext _context;

        public RatingRepository(CarBuddyContext context) => _context = context;

        public void AddRating(Rating rating)
        {
            _context.Ratings.Add(rating);
            _context.SaveChanges();
        }

        public bool HasRating(Guid driverId, Guid passengerId) =>
            _context.Ratings.Any(r => r.AuthorId == passengerId && r.RecipientId == driverId);
    }
}
