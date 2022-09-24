using CarBuddy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarBuddy.Persistence.Configurations
{
    public class TripRequestEntityTypeConfiguration : IEntityTypeConfiguration<TripRequest>
    {
        public void Configure(EntityTypeBuilder<TripRequest> builder)
        {
            builder.ToTable("tripRequest")
                .HasKey(tr => new { tr.TripId, tr.PassengerId });

            builder.HasOne(tr => tr.Trip)
                .WithMany(t => t.TripRequests)
                .HasForeignKey(tr => tr.TripId);

            builder.HasOne(tr => tr.Passenger)
                .WithMany(u => u.TripRequests)
                .HasForeignKey(tr => tr.PassengerId);

            builder.Property(tr => tr.TripId)
                .HasColumnName("tripId");

            builder.Property(tr => tr.PassengerId)
                .HasColumnName("passengerId");

            builder.Property(tr => tr.NumberOfPassengers)
                .HasColumnName("numberOfPassengers")
                .IsRequired();

            builder.Property(tr => tr.Status)
                .HasColumnName("status")
                .IsRequired();
        }
    }
}
