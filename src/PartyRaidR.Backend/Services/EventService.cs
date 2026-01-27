using PartyRaidR.Backend.Repos.Promises;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Assemblers;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;
using PartyRaidR.Shared.Models.Responses;

namespace PartyRaidR.Backend.Services
{
    public class EventService : BaseService<Event, EventDto>, IEventService
    {
        public EventService(EventAssembler assembler, IEventRepo? repo, IUserContext userContext) : base(assembler, repo, userContext)
        {
        }

        public override async Task<ServiceResponse<EventDto>> AddAsync(EventDto dto)
        {
            if(dto.Title == string.Empty)
                throw new ArgumentException("Title cannot be empty.");

            if(dto.Description == string.Empty)
                throw new ArgumentException("Description cannot be empty.");

            dto.AuthorId = _userContext.UserId;

            return await base.AddAsync(dto);
        }
    }
}
