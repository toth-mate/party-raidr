using PartyRaidR.Backend.Context;
using PartyRaidR.Backend.Repos.Base;
using PartyRaidR.Backend.Repos.Promises;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Backend.Repos
{
    public class ApplicationRepo : RepositoryBase<Application>, IApplicationRepo
    {
        public ApplicationRepo(AppDbContext? context) : base(context)
        {
        }
    }
}
