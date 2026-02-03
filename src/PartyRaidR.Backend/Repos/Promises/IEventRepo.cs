using PartyRaidR.Backend.Repos.Base;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Backend.Repos.Promises
{
    public interface IEventRepo : IRepositoryBase<Event>
    {
        Task<List<Event>> GetEventsByUserIdAsync(string userId);
        Task<List<Event>> FilterEventsAsync(string? title,
                                       string? description,
                                       DateTime? startingDate,
                                       DateTime? endingDate,
                                       string? placeId,
                                       string? cityId,
                                       EventCategory? category,
                                       decimal? ticketPriceMin,
                                       decimal? ticketPriceMax);
    }
}
