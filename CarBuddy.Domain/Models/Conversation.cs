using System;
using System.Collections.Generic;
using System.Linq;

namespace CarBuddy.Domain.Models
{
    public class Conversation : Entity
    {
        public static Conversation Empty => new Conversation();

        public DateTime StartDate { get; private set; }
        public Guid FirstParticipantId { get; private set; }
        public User FirstParticipant { get; private set; }
        public Guid SecondParticipantId { get; private set; }
        public User SecondParticipant { get; private set; }
        public ConversationStatus FirstParticipantReadStatus { get; private set; }
        public ConversationStatus SecondParticipantReadStatus { get; private set; }
        public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

        public ChatMessage LastMessage => ChatMessages.FirstOrDefault() ?? ChatMessage.Empty;

        public bool IsEmpty => Id == Empty.Id;

        public Guid LastRecipientId => LastMessage.AuthorId == FirstParticipantId
            ? SecondParticipantId
            : FirstParticipantId;

        public Conversation()
        { }

        public Conversation(Guid authorId, Guid participantId)
        {
            FirstParticipantId = authorId;
            SecondParticipantId = participantId;
        }

        public bool IsParticipant(Guid participantId) =>
            IsFirstParticipant(participantId) || IsSecondParticipant(participantId);

        public void MarkAsUnread()
        {
            if (LastRecipientId == FirstParticipantId)
            {
                FirstParticipantReadStatus = ConversationStatus.Unread;
                return;
            }

            SecondParticipantReadStatus = ConversationStatus.Unread;
        }

        public void MarkAsRead(Guid participantId)
        {
            if (participantId == FirstParticipantId)
            {
                FirstParticipantReadStatus = ConversationStatus.Read;
                return;
            }

            SecondParticipantReadStatus = ConversationStatus.Read;
        }

        public void MarkAsRead()
        {
            if (LastRecipientId == FirstParticipantId)
            {
                FirstParticipantReadStatus = ConversationStatus.Read;
                return;
            }

            SecondParticipantReadStatus = ConversationStatus.Read;
        }

        public bool IsUnread(Guid userId)
        {
            var status = userId == FirstParticipantId
                ? FirstParticipantReadStatus
                : SecondParticipantReadStatus;

            return status == ConversationStatus.Unread;
        }

        private bool IsFirstParticipant(Guid participantId) =>
            FirstParticipantId == participantId && SecondParticipantId != participantId;

        private bool IsSecondParticipant(Guid participantId) =>
            SecondParticipantId == participantId && FirstParticipantId != participantId;
    }
}
