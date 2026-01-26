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
        public EventController(IEventService service) : base(service)
        {
        }
    }
}
