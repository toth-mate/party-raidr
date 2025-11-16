using PartyRaidR.Backend.Context;
using PartyRaidR.Backend.Repos.Base;
using PartyRaidR.Backend.Repos.Promises;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Backend.Repos
{
    public class PlaceRepo : RepositoryBase<Place>, IPlaceRepo
    {
        public PlaceRepo(AppDbContext? context) : base(context)
        {
        }
    }
}
