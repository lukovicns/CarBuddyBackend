using CarBuddy.Application;
using CarBuddy.Application.Models;
using CarBuddy.Application.Models.DTOs;
using CarBuddy.Application.Services;
using CarBuddy.Domain.Models;
using CarBuddy.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;

namespace CarBuddy.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessagesController : ControllerBase
    {
        private readonly MessageService _messageService;
        private readonly UserService _userService;
        private readonly ConversationService _conversationService;
        private readonly IHubContext<ChatHub, IChatClient> _chatHub;

        public MessagesController(
            MessageService messageService,
            UserService userService,
            ConversationService conversationService,
            IHubContext<ChatHub, IChatClient> chatHub)
        {
            _messageService = messageService;
            _userService = userService;
            _conversationService = conversationService;
            _chatHub = chatHub;
        }

        [HttpGet("{participantId:guid}/conversation/{conversationId:guid}")]
        public IActionResult GetMessages(Guid participantId, Guid conversationId, [FromQuery] Pagination pagination)
        {
            var messagesDto = _messageService.GetMessages(participantId, conversationId)
                .Select(message => new ChatMessageDto(participantId, message));
            var result = new PagedResult<ChatMessageDto>(messagesDto, pagination);

            return Ok(new
            {
                Content = result.Content.Reverse(),
                result.Pagination,
            });
        }

        [HttpPost("send")]
        public IActionResult SendMessage([FromBody] SentMessage sentMessage)
        {
            var author = _userService.GetUserById(sentMessage.AuthorId);

            if (author.IsEmpty)
                return NotFound(Constants.UserNotFound);

            var conversation = _conversationService.GetConversationById(sentMessage.ConversationId);

            if (conversation.IsEmpty && sentMessage.ParticipantId != Guid.Empty)
            {
                conversation = _conversationService.CreateNewConversation(sentMessage);
                sentMessage.ConversationId = conversation.Id;
            }

            if (conversation.IsEmpty)
                return NotFound(Constants.ConversationNotFound);

            var message = _messageService.Send(sentMessage);
            var messageDto = new ChatMessageDto(message.AuthorId, message);
            var conversationDto = new ConversationDto(message.AuthorId, message.Conversation);
            NotifyClient(conversation, messageDto, conversationDto);

            return Ok(new { Message = messageDto, Conversation = conversationDto });
        }

        private void NotifyClient(Conversation conversation, ChatMessageDto messageDto, ConversationDto conversationDto)
        {
            _chatHub.Clients.All.ReceiveMessage(messageDto, messageDto.ParticipantId);
            _chatHub.Clients.All.UpdateConversation(conversationDto);
            _chatHub.Clients.All.ReceiveNotification(conversation.LastRecipientId);
        }
    }
}
