using CarBuddy.Domain.Models;
using System;

namespace CarBuddy.Application.Models.DTOs
{
    public class ChatMessageDto
    {
        public Guid ConversationId { get; set; }
        public Guid AuthorId { get; set; }
        public Guid UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserPhoto { get; set; }
        public Guid ParticipantId { get; set; }
        public string ParticipantFirstName { get; set; }
        public string ParticipantLastName { get; set; }
        public string ParticipantPhoto { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public ChatMessageDto() { }

        public ChatMessageDto(Guid userId, ChatMessage message)
        {
            ConversationId = message.ConversationId;
            AuthorId = message.AuthorId;
            Message = message.Message;
            Date = message.Date;

            if (userId == message.Conversation.FirstParticipantId)
            {
                AppendUser(message.Conversation.FirstParticipant);
                AppendParticipant(message.Conversation.SecondParticipant);
                return;
            }

            AppendUser(message.Conversation.SecondParticipant);
            AppendParticipant(message.Conversation.FirstParticipant);
        }

        private void AppendUser(User user)
        {
            UserId = user.Id;
            UserFirstName = user.FirstName;
            UserLastName = user.LastName;
            UserPhoto = user.Photo;
        }

        private void AppendParticipant(User participant)
        {
            ParticipantId = participant.Id;
            ParticipantFirstName = participant.FirstName;
            ParticipantLastName = participant.LastName;
            ParticipantPhoto = participant.Photo;
        }
    }
}
