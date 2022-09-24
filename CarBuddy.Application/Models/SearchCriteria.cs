using System;
using System.ComponentModel.DataAnnotations;

namespace CarBuddy.Application.Models
{
    public class SearchCriteria
    {
        [Required]
        public string FromCity { get; set; }

        [Required]
        public string ToCity { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(1, 8)]
        public int NumberOfPassengers { get; set; }
    }
}
