using PartyRaidR.Backend.Repos.Base;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Backend.Repos.Promises
{
    public interface IEventRepo : IRepositoryBase<Event>
    {
        Task<List<Event>> GetEventsByUserIdAsync(string userId);
    }
}
