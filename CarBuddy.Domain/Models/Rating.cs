using System;
using System.Collections.Generic;

namespace CarBuddy.Domain.Models
{
    public class Rating : Entity
    {
        public Guid AuthorId { get; set; }
        public User Author { get; set; }
        public Guid RecipientId { get; set; }
        public User Recipient { get; set; }
        public double Evaluation { get; set; }

        public virtual ICollection<RatingComment> RatingComments { get; set; } = new List<RatingComment>();

        public Rating() { }

        public Rating(Guid authorId, Guid recipientId, double evaluation, string comment)
        {
            Id = Guid.NewGuid();
            AuthorId = authorId;
            RecipientId = recipientId;
            Evaluation = evaluation;
            AddComment(comment);
        }

        public void AddComment(string comment) =>
            RatingComments.Add(new RatingComment(Id, comment));
    }
}
