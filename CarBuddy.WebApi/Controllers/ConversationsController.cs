using CarBuddy.Application;
using CarBuddy.Application.Models;
using CarBuddy.Application.Models.DTOs;
using CarBuddy.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CarBuddy.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ConversationsController : ControllerBase
    {
        private readonly ConversationService _conversationService;

        public ConversationsController(ConversationService conversationService) => _conversationService = conversationService;

        [HttpGet("{participantId:guid}")]
        public IActionResult GetConversations(Guid participantId, [FromQuery] Pagination pagination)
        {
            var conversationsDto = _conversationService.GetConversations(participantId)
                .Select(conversation => new ConversationDto(participantId, conversation));
            var result = new PagedResult<ConversationDto>(conversationsDto, pagination);

            return Ok(new
            {
                result.Content,
                result.Pagination,
            });
        }

        [HttpGet("conversation-id")]
        public IActionResult GetConversationId([FromQuery] Guid firstParticipantId, [FromQuery] Guid secondParticipantId)
        {
            if (firstParticipantId == Guid.Empty || secondParticipantId == Guid.Empty)
                return NotFound(new { Message = Constants.ConversationNotFound });

            var conversation = _conversationService.GetConversation(firstParticipantId, secondParticipantId);

            return Ok(new { conversationId = conversation.Id });
        }

        [HttpPost("{conversationId:guid}/mark-as-read")]
        public IActionResult MarkAsRead(Guid conversationId)
        {
            var conversation = _conversationService.GetConversationById(conversationId, true);

            if (conversation.IsEmpty)
                return NotFound(Constants.ConversationNotFound);

            _conversationService.MarkAsRead(conversation);
            return Ok();
        }
    }
}
