using CarBuddy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarBuddy.Persistence.Configurations
{
    public class ConversationEntityTypeConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.ToTable("conversation")
                .HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .HasColumnName("id");

            builder.Property(c => c.StartDate)
                .HasColumnName("startDate");

            builder.HasOne(c => c.FirstParticipant)
                .WithMany(c => c.FirstParticipantConversations)
                .HasForeignKey(c => c.FirstParticipantId);

            builder.Property(c => c.FirstParticipantId)
                .HasColumnName("firstParticipantId");

            builder.HasOne(c => c.SecondParticipant)
                .WithMany(c => c.SecondParticipantConversations)
                .HasForeignKey(c => c.SecondParticipantId);

            builder.Property(c => c.SecondParticipantId)
                .HasColumnName("secondParticipantId");

            builder.Property(m => m.FirstParticipantReadStatus)
                .HasColumnName("firstParticipantReadStatus");

            builder.Property(m => m.SecondParticipantReadStatus)
            .HasColumnName("secondParticipantReadStatus");
        }
    }
}
