using CarBuddy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarBuddy.Persistence.Configurations
{
    public class CarEntityTypeConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("car")
                .HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("id");

            builder.Property(c => c.DriverId)
                .HasColumnName("driverId");

            builder.Property(c => c.Brand)
                .HasColumnName("brand");

            builder.Property(c => c.Model)
                .HasColumnName("model");

            builder.Property(c => c.Photo)
                .HasColumnName("photo");

            builder.Property(c => c.Year)
                .HasColumnName("year");

            builder.Property(c => c.NumberOfSeats)
                .HasColumnName("numberOfSeats");
        }
    }
}
