using CarBuddy.Application.Contracts.Repositories;
using CarBuddy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarBuddy.Persistence.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly CarBuddyContext _context;

        public ConversationRepository(CarBuddyContext context) => _context = context;

        public IEnumerable<Conversation> GetConversations(Guid participantId)
        {
            return _context.Conversations
                .Include(c => c.FirstParticipant)
                .Include(c => c.SecondParticipant)
                .Include(c => c.ChatMessages.OrderByDescending(m => m.Date))
                .AsEnumerable()
                .Where(c => c.IsParticipant(participantId))
                .OrderByDescending(m => m.ChatMessages.First().Date);
        }

        public int TotalElements(Guid participantId) =>
            GetConversations(participantId).Count();

        public Conversation GetConversationById(Guid id, bool includeLastMessage)
        {
            var conversation = _context.Conversations
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (conversation == null)
                return Conversation.Empty;

            if (includeLastMessage)
            {
                _context.Entry(conversation)
                .Collection(c => c.ChatMessages)
                .Query()
                .OrderByDescending(m => m.Date)
                .Take(1)
                .Load();
            }

            return conversation;
        }

        public void MarkAsRead(Conversation conversation)
        {
            conversation.MarkAsRead();
            _context.Update(conversation);
            _context.SaveChanges();
        }

        public void MarkAsUnread(Conversation conversation)
        {
            conversation.MarkAsUnread();
            _context.Update(conversation);
            _context.SaveChanges();
        }

        public int GetNumberOfUnreadConversations(Guid userId)
        {
            return GetConversations(userId)
                .Count(conversation => conversation.IsUnread(userId));
        }

        public Conversation CreateNewConversation(SentMessage sentMessage)
        {
            var conversationEntry = _context.Conversations.Add(new Conversation(sentMessage.AuthorId, sentMessage.ParticipantId));
            var conversation = conversationEntry.Entity;
            conversation.MarkAsRead(sentMessage.AuthorId);
            _context.SaveChanges();
            return conversationEntry.Entity;
        }

        public bool ConversationExists(Guid authorId, Guid recipientId)
        {
            var conversation = GetConversation(authorId, recipientId);
            return !conversation.IsEmpty;
        }

        public Conversation GetConversation(Guid firstParticipantId, Guid secondParticipantId)
        {
            return _context.Conversations
                    .Where(conversation =>
                        (conversation.FirstParticipantId == firstParticipantId && conversation.SecondParticipantId == secondParticipantId)
                        || (conversation.FirstParticipantId == secondParticipantId && conversation.SecondParticipantId == firstParticipantId))
                    .FirstOrDefault() ?? Conversation.Empty;
        }
    }
}
