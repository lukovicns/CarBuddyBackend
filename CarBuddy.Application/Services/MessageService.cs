using CarBuddy.Application.Contracts.Repositories;
using CarBuddy.Domain.Models;
using System;
using System.Collections.Generic;

namespace CarBuddy.Application.Services
{
    public class MessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public IEnumerable<ChatMessage> GetMessages(Guid participantId, Guid conversationId) =>
            _messageRepository.GetMessages(participantId, conversationId);

        public ChatMessage Send(SentMessage message) =>
            _messageRepository.Send(message);
    }
}
