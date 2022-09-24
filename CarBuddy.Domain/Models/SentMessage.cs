using System;

namespace CarBuddy.Domain.Models
{
    public class SentMessage
    {
        public Guid AuthorId { get; set; }
        public Guid ParticipantId { get; set; }
        public Guid ConversationId { get; set; }
        public string Message { get; set; }
    }
}
