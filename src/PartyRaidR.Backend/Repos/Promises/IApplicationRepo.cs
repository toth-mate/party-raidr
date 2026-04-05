using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Repos.Base;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Repos.Promises
{
    public interface IApplicationRepo : IRepositoryBase<Application>
    {
        Task<bool> ApplicationExistsAsync(string userId, string eventId);
        Task<List<Application>> GetApplicationsByUserAsync(string userId);
        Task<Application?> GetApplicationWithEventAsync(string id);
        IQueryable<Application> GetApplicationDisplaysQueryable();
    }
}
