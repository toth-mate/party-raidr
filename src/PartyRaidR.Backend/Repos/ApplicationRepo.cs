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
    }
}
