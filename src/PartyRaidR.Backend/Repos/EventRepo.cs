using Microsoft.EntityFrameworkCore;
using PartyRaidR.Backend.Context;
using PartyRaidR.Backend.Repos.Base;
using PartyRaidR.Backend.Repos.Promises;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Backend.Repos
{
    public class EventRepo : RepositoryBase<Event>, IEventRepo
    {
        public EventRepo(AppDbContext? context) : base(context)
        {
        }

        public async Task<List<Event>> GetEventsByUserIdAsync(string userId) =>
            await _dbSet!.Include(e => e.Place)
                         .Where(e => e.User.Id == userId)
                         .ToListAsync();
    }
}
