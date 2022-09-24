using CarBuddy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarBuddy.Persistence.Configurations
{
    public class RatingCommentEntityTypeConfiguration : IEntityTypeConfiguration<RatingComment>
    {
        public void Configure(EntityTypeBuilder<RatingComment> builder)
        {
            builder.ToTable("ratingComment")
                .HasKey(r => r.Id);

            builder.HasOne(rc => rc.Rating)
                .WithMany(r => r.RatingComments)
                .HasForeignKey(rc => rc.RatingId);

            builder.Property(r => r.Id)
                .HasColumnName("id");

            builder.Property(r => r.RatingId)
                .HasColumnName("ratingId");
            
            builder.Property(r => r.Description)
                .HasColumnName("description");
        }
    }
}
