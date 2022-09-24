using CarBuddy.Application.Models.DTOs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace CarBuddy.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(ChatMessageDto message, Guid recipientId);

        Task UpdateConversation(ConversationDto conversation);

        Task ReceiveNotification(Guid recipientId);
    }

    public class ChatHub : Hub<IChatClient> { }
}
