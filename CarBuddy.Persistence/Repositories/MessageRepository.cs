using CarBuddy.Application.Contracts.Repositories;
using CarBuddy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarBuddy.Persistence.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly CarBuddyContext _context;
        private readonly IConversationRepository _conversationRepository;

        public MessageRepository(CarBuddyContext context,
            IConversationRepository conversationRepository)
        {
            _context = context;
            _conversationRepository = conversationRepository;
        }

        public IEnumerable<ChatMessage> GetMessages(Guid participantId, Guid conversationId)
        {
            return _context.Messages
                .Where(m => m.ConversationId == conversationId)
                .Include(m => m.Conversation)
                .ThenInclude(m => m.FirstParticipant)
                .Include(m => m.Conversation)
                .ThenInclude(m => m.SecondParticipant)
                .Include(m => m.Author)
                .Where(m => m.Conversation.FirstParticipantId == participantId
                            || m.Conversation.SecondParticipantId == participantId)
                .OrderByDescending(m => m.Date);
        }

        public int TotalMessages(Guid participantId, Guid conversationId)
        {
            return GetMessages(participantId, conversationId)
                .Count();
        }

        public ChatMessage Send(SentMessage sentMessage)
        {
            var message = new ChatMessage(sentMessage);
            _context.Messages.Add(message);
            _context.SaveChanges();

            var conversation = _conversationRepository.GetConversationById(sentMessage.ConversationId);
            _conversationRepository.MarkAsUnread(conversation);

            return GetMessage(message.Id);
        }

        private ChatMessage GetMessage(Guid messageId) =>
            _context.Messages
                .Include(m => m.Author)
                .Include(m => m.Conversation)
                .ThenInclude(m => m.FirstParticipant)
                .Include(m => m.Conversation)
                .ThenInclude(m => m.SecondParticipant)
                .First(m => m.Id == messageId);
    }
}
