using PartyRaidR.Backend.Context;
using PartyRaidR.Backend.Repos.Base;
using PartyRaidR.Backend.Repos.Promises;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Backend.Repos
{
    public class CityRepo : RepositoryBase<City>, ICityRepo
    {
        public CityRepo(AppDbContext? context) : base(context)
        {
        }
    }
}
