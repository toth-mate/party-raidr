using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Dtos;

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

        [HttpGet("count")]
        public async Task<IActionResult> GetEventCount() =>
            HandleResponse(await _eventService.GetNumberOfEventsAsync());

        [HttpGet("count/active")]
        public async Task<IActionResult> GetActiveEventCount() =>
            HandleResponse(await _eventService.GetNumberOfActiveEventsAsync());

        [HttpGet("count/archived")]
        public async Task<IActionResult> GetArchivedEventCount() =>
            HandleResponse(await _eventService.GetNumberOfArchivedEventsAsync());

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

        [HttpPost("filter")]
        public async Task<IActionResult> FilterEvents([FromBody] EventFilterDto filter) =>
            HandleResponse(await _eventService.FilterEventsAsync(filter));
    }
}
