using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;
using PartyRaidR.Shared.Models.Responses;

namespace PartyRaidR.Backend.Services.Promises
{
    public interface IEventService : IBaseService<Event, EventDto>
    {
        Task<ServiceResponse<List<UpcomingEventDto>>> GetUpcomingEventsAsync();
        Task<ServiceResponse<List<EventDto>>> GetEventsByUserIdAsync(string userId);
        Task<ServiceResponse<List<EventDto>>> GetMyEventsAsync();
        Task<ServiceResponse<List<EventDto>>> FilterEventsAsync(EventFilterDto filter);
    }
}
