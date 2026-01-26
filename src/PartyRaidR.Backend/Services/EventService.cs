using PartyRaidR.Backend.Repos.Promises;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Assemblers;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Backend.Services
{
    public class EventService : BaseService<Event, EventDto>, IEventService
    {
        public EventService(EventAssembler assembler, IEventRepo? repo, IUserContext userContext) : base(assembler, repo, userContext)
        {
        }
    }
}
