using CarBuddy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarBuddy.Persistence.Configurations
{
    public class UserTripEntityTypeConfiguration : IEntityTypeConfiguration<UserTrip>
    {
        public void Configure(EntityTypeBuilder<UserTrip> builder)
        {
            builder.ToTable("userTrip")
                .HasKey(t => new { t.UserId, t.TripId });

            builder.HasOne(u => u.User)
                .WithMany(ut => ut.UserTrips)
                .HasForeignKey(ut => ut.UserId);

            builder.HasOne(u => u.Trip)
                .WithMany(ut => ut.UserTrips)
                .HasForeignKey(ut => ut.TripId);

            builder.Property(ut => ut.UserId)
                .HasColumnName("userId");

            builder.Property(ut => ut.TripId)
                .HasColumnName("tripId");
        }
    }
}
