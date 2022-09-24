using System;

namespace CarBuddy.Domain.Models
{
    public class RatingComment : Entity
    {
        public Guid RatingId { get; set; }
        public Rating Rating { get; set; }
        public string Description { get; set; }

        public RatingComment(Guid ratingId, string description)
        {
            Id = Guid.NewGuid();
            RatingId = ratingId;
            Description = description;
        }
    }
}
