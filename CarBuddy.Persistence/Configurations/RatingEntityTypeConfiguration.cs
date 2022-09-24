using CarBuddy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarBuddy.Persistence.Configurations
{
    public class RatingEntityTypeConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("rating")
                .HasKey(r => r.Id);

            builder.HasOne(r => r.Author)
                .WithMany(u => u.AuthorRatings)
                .HasForeignKey(r => r.AuthorId);

            builder.HasOne(r => r.Recipient)
                .WithMany(u => u.RecipientRatings)
                .HasForeignKey(r => r.RecipientId);

            builder.Property(r => r.Id)
                .HasColumnName("id");
            
            builder.Property(r => r.AuthorId)
                .HasColumnName("authorId");

            builder.Property(r => r.RecipientId)
                .HasColumnName("recipientId");
            
            builder.Property(r => r.Evaluation)
                .HasColumnName("evaluation");
        }
    }
}
