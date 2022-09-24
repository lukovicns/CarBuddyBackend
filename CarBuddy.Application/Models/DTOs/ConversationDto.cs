using CarBuddy.Domain.Models;
using System;

namespace CarBuddy.Application.Models.DTOs
{
    public class ConversationDto
    {
        public Guid Id { get; set; }
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
        public ConversationStatus Status { get; set; }

        public ConversationDto()
        { }

        public ConversationDto(Guid userId, Conversation conversation)
        {
            var chatMessage = new ChatMessageDto(userId, conversation.LastMessage);

            Id = conversation.Id;
            Message = new Summary(chatMessage.Message, 25);
            Date = chatMessage.Date;
            UserId = chatMessage.UserId;
            UserFirstName = chatMessage.UserFirstName;
            UserLastName = chatMessage.UserLastName;
            UserPhoto = chatMessage.UserPhoto;
            ParticipantId = chatMessage.ParticipantId;
            ParticipantFirstName = chatMessage.ParticipantFirstName;
            ParticipantLastName = chatMessage.ParticipantLastName;
            ParticipantPhoto = chatMessage.ParticipantPhoto;
            Status = UserId == conversation.FirstParticipantId
                ? conversation.FirstParticipantReadStatus
                : conversation.SecondParticipantReadStatus;
        }
    }
}
