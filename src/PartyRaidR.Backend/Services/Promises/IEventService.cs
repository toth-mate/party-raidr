using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Models.Responses;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Services.Promises
{
    public interface IEventService : IBaseService<Event, EventDto>
    {
        Task<ServiceResponse<List<UpcomingEventDto>>> GetUpcomingEventsAsync();
        Task<ServiceResponse<List<EventDto>>> GetEventsByUserIdAsync(string userId);
        Task<ServiceResponse<List<EventDto>>> GetMyEventsAsync();
        Task<ServiceResponse<List<EventDto>>> FilterEventsAsync(EventFilterDto filter);
        Task<ServiceResponse<EventDto>> ArchiveOldEventsAsync();
    }
}
