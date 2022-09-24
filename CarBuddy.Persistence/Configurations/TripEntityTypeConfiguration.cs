using CarBuddy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarBuddy.Persistence.Configurations
{
    public class TripEntityTypeConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.ToTable("trip")
                .HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("id");

            builder.Property(t => t.DriverId)
                .HasColumnName("driverId");

            builder.Property(t => t.FromCity)
                .HasColumnName("fromCity")
                .IsRequired();

            builder.Property(t => t.ToCity)
               .HasColumnName("toCity")
               .IsRequired();

            builder.Property(t => t.Date)
                .HasColumnName("date")
                .IsRequired();

            builder.Property(t => t.StartTime)
                .HasColumnName("startTime")
                .IsRequired();

            builder.Property(t => t.ArriveTime)
                .HasColumnName("arriveTime")
                .IsRequired();

            builder.Property(t => t.Price)
                .HasColumnName("price")
                .IsRequired();

            builder.Property(t => t.NumberOfPassengers)
                .HasColumnName("numberOfPassengers");

            builder.Property(t => t.IsDeleted)
                .HasColumnName("isDeleted");

            builder.HasOne(t => t.Driver)
                .WithMany(u => u.DriverTrips)
                .HasForeignKey(t => t.DriverId);

            builder.Ignore(t => t.NumberOfAvailableSeats);
            builder.Ignore(t => t.IsEmpty);
            builder.Ignore(t => t.UserTrips);
        }
    }
}
