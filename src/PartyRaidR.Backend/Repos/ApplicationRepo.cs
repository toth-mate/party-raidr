using Microsoft.EntityFrameworkCore;
using PartyRaidR.Backend.Context;
using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Repos.Base;
using PartyRaidR.Backend.Repos.Promises;

namespace PartyRaidR.Backend.Repos
{
    public class ApplicationRepo : RepositoryBase<Application>, IApplicationRepo
    {
        public ApplicationRepo(AppDbContext? context) : base(context)
        {
        }

        public async Task<bool> ApplicationExistsAsync(string userId, string eventId) =>
            await _dbSet!.AnyAsync(a => a.UserId == userId && a.EventId == eventId);

        public async Task<List<Application>> GetApplicationsByUserAsync(string userId) =>
            await _dbSet!.Where(a => a.UserId == userId).ToListAsync();

        public async Task<Application?> GetApplicationWithEventAsync(string id) =>
            await _dbSet!.Include(a => a.Event)
                         .FirstOrDefaultAsync(a => a.Id == id);
    }
}
