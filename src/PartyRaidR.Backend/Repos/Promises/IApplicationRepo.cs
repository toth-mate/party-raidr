using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Repos.Base;

namespace PartyRaidR.Backend.Repos.Promises
{
    public interface IApplicationRepo : IRepositoryBase<Application>
    {
        Task<bool> ApplicationExistsAsync(string userId, string eventId);
        Task<Application?> GetApplicationWithEventAsync(string id);
    }
}
