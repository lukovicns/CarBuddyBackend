using CarBuddy.Domain.Models;
using System;
using System.Collections.Generic;

namespace CarBuddy.Application.Contracts.Repositories
{
    public interface IMessageRepository
    {
        IEnumerable<ChatMessage> GetMessages(Guid participantId, Guid conversationId);

        int TotalMessages(Guid participantId, Guid conversationId);

        ChatMessage Send(SentMessage message);
    }
}
