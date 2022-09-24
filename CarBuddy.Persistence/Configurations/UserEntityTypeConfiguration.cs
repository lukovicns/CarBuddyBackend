using CarBuddy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarBuddy.Persistence.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user")
                .HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id");

            builder.HasOne(u => u.Car)
                .WithOne(c => c.Driver)
                .HasForeignKey<Car>(c => c.DriverId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(u => u.CarId)
                .HasColumnName("carId");

            builder.Property(u => u.FirstName)
                .HasColumnName("firstName");

            builder.Property(u => u.LastName)
                .HasColumnName("lastName");

            builder.Property(u => u.Email)
                .HasColumnName("email");

            builder.Property(u => u.Password)
                .HasColumnName("password");

            builder.Property(u => u.Age)
                .HasColumnName("age");

            builder.Property(u => u.Photo)
                .HasColumnName("photo");

            builder.Property(u => u.IsActivated)
                .HasColumnName("isActivated");

            builder.Property(u => u.CreatedAt)
                .HasColumnName("createdAt");

            builder.Property(u => u.ActivationToken)
                .HasColumnName("activationToken");

            builder.Ignore(u => u.IsEmpty);
            builder.Ignore(u => u.DriverTrips);
            builder.Ignore(u => u.UserTrips);
            builder.Ignore(u => u.ChatMessages);
        }
    }
}
