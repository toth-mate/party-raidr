using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Repos.Base;
using PartyRaidR.Shared.Enums;

namespace PartyRaidR.Backend.Repos.Promises
{
    public interface IEventRepo : IRepositoryBase<Event>
    {
        Task<Event> GetEventWithDisplayData(string id);
        Task<List<Event>> GetEventsWithDisplayData();
        Task<List<Event>> GetEventsByUserIdAsync(string userId);
        Task<List<Event>> FilterEventsAsync(string? title,
                                       string? description,
                                       DateTime? startingDate,
                                       DateTime? endingDate,
                                       string? placeName,
                                       string? placeId,
                                       string? cityId,
                                       EventCategory? category,
                                       decimal? ticketPriceMin,
                                       decimal? ticketPriceMax);
    }
}
