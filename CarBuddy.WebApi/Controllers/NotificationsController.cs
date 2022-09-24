using CarBuddy.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarBuddy.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly ConversationService _conversationService;
        private readonly TripRequestService _tripRequestService;

        public NotificationsController(ConversationService conversationService, TripRequestService tripRequestService)
        {
            _conversationService = conversationService;
            _tripRequestService = tripRequestService;
        }

        [HttpGet("{userId:guid}")]
        public IActionResult GetNotifications(Guid userId)
        {
            return Ok(_conversationService.GetNumberOfUnreadConversations(userId));
        }

        [HttpGet("trip-requests/{userId:guid}")]
        public IActionResult GetTripRequestsCount(Guid userId)
        {
            return Ok(_tripRequestService.GetTripRequestsCount(userId));
        }
    }
}
