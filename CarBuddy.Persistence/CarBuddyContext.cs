using CarBuddy.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarBuddy.Persistence
{
    public class CarBuddyContext : DbContext
    {
        public CarBuddyContext(DbContextOptions<CarBuddyContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripRequest> TripRequests { get; set; }
        public DbSet<UserTrip> UserTrips { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<RatingComment> RatingComments { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ChatMessage> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
