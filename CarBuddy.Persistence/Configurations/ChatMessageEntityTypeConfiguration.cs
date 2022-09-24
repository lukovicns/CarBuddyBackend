using CarBuddy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarBuddy.Persistence.Configurations
{
    public class ChatMessageEntityTypeConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.ToTable("message")
                .HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .HasColumnName("id");

            builder.HasOne(m => m.Conversation)
                .WithMany(m => m.ChatMessages)
                .HasForeignKey(m => m.ConversationId);

            builder.Property(m => m.ConversationId)
                .HasColumnName("conversationId");

            builder.HasOne(m => m.Author)
                .WithMany(m => m.ChatMessages)
                .HasForeignKey(m => m.AuthorId);

            builder.Property(m => m.AuthorId)
                .HasColumnName("authorId");

            builder.Property(m => m.Message)
                .HasColumnName("message");

            builder.Property(m => m.Date)
                .HasColumnName("date");
        }
    }
}
