using System;

namespace CarBuddy.Domain.Models
{
    public class ChatMessage : Entity
    {
        public static ChatMessage Empty => new ChatMessage();

        public Guid ConversationId { get; set; }
        public Conversation Conversation { get; set; }
        public Guid AuthorId { get; set; }
        public User Author { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public ChatMessage() { }

        public ChatMessage(SentMessage message)
        {
            AuthorId = message.AuthorId;
            ConversationId = message.ConversationId;
            Message = message.Message;
            Date = DateTime.Now;
        }

        public void SetConversation(Conversation conversation)
        {
            ConversationId = conversation.Id;
            Conversation = conversation;
        }
    }
}
