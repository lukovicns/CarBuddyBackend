using System;

namespace CarBuddy.Domain.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}
