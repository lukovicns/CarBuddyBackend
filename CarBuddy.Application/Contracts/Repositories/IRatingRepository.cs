using CarBuddy.Domain.Models;

namespace CarBuddy.Application.Contracts.Repositories
{
    public interface IRatingRepository
    {
        void AddRating(Rating rating);
    }
}
