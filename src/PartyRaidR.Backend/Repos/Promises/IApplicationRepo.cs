using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Repos.Base;

namespace PartyRaidR.Backend.Repos.Promises
{
    public interface IApplicationRepo : IRepositoryBase<Application>
    {
        Task<bool> ApplicationExistsAsync(string userId, string eventId);
        Task<List<Application>> GetApplicationsByUserAsync(string userId);
        Task<Application?> GetApplicationWithEventAsync(string id);
    }
}
