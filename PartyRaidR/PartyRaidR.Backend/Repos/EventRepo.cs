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
    }
}
