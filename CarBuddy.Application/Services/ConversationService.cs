using CarBuddy.Application.Contracts.Repositories;
using CarBuddy.Application.Exceptions;
using CarBuddy.Domain.Models;
using System;
using System.Collections.Generic;

namespace CarBuddy.Application.Services
{
    public class ConversationService
    {
        private readonly IConversationRepository _conversationRepository;

        public ConversationService(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }

        public IEnumerable<Conversation> GetConversations(Guid participantId)
            => _conversationRepository.GetConversations(participantId);

        public Conversation GetConversationById(Guid id, bool includeLastMessage = false) =>
            _conversationRepository.GetConversationById(id, includeLastMessage);

        public void MarkAsRead(Conversation conversation)
        {
            _conversationRepository.MarkAsRead(conversation);
        }

        public int GetNumberOfUnreadConversations(Guid userId) =>
            _conversationRepository.GetNumberOfUnreadConversations(userId);

        public Conversation CreateNewConversation(SentMessage sentMessage)
        {
            var exists = _conversationRepository.ConversationExists(sentMessage.AuthorId, sentMessage.ParticipantId);

            if (exists)
                throw new EntityAlreadyExists(nameof(Conversation));

            return _conversationRepository.CreateNewConversation(sentMessage);
        }

        public Conversation GetConversation(Guid firstParticipantId, Guid secondParticipantId) =>
            _conversationRepository.GetConversation(firstParticipantId, secondParticipantId);
    }
}
