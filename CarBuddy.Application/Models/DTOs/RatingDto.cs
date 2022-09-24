using CarBuddy.Application.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace CarBuddy.Application.Models.DTOs
{
    public class RatingDto
    {
        [GuidRequired]
        public Guid TripId { get; set; }

        [GuidRequired]
        public Guid AuthorId { get; set; }
        
        [GuidRequired]
        public Guid RecipientId { get; set; }
        
        [Required]
        [Range(0.5, 5)]
        public double Evaluation { get; set; }

        [Required]
        [StringLength(512)]
        public string Comment { get; set; }
    }
}
