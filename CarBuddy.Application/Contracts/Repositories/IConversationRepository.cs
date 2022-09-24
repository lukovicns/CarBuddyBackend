using CarBuddy.Domain.Models;
using System;
using System.Collections.Generic;

namespace CarBuddy.Application.Contracts.Repositories
{
    public interface IConversationRepository
    {
        IEnumerable<Conversation> GetConversations(Guid participantId);

        int TotalElements(Guid participantId);

        Conversation GetConversationById(Guid id, bool includeLastMessage = false);

        void MarkAsRead(Conversation conversation);

        void MarkAsUnread(Conversation conversation);

        int GetNumberOfUnreadConversations(Guid userId);

        Conversation CreateNewConversation(SentMessage sentMessage);

        bool ConversationExists(Guid authorId, Guid recipientId);

        Conversation GetConversation(Guid firstParticipantId, Guid secondParticipantId);
    }
}
