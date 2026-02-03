using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : BaseController<Event, EventDto>
    {
        private readonly IEventService _eventService;

        public EventController(IEventService service) : base(service)
        {
            _eventService = service;
        }

        [HttpGet("upcoming-events")]
        public async Task<IActionResult> GetUpcomingEvents() =>
            HandleResponse(await _eventService.GetUpcomingEventsAsync());

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetEventsByUserId(string userId) =>
            HandleResponse(await _eventService.GetEventsByUserIdAsync(userId));

        [Authorize]
        [HttpGet("my-events")]
        public async Task<IActionResult> GetMyEvents() =>
            HandleResponse(await _eventService.GetMyEventsAsync());
    }
}
