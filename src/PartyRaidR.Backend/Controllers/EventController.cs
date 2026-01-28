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
    }
}
