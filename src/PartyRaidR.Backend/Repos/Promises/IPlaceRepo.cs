using PartyRaidR.Backend.Repos.Base;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Backend.Repos.Promises
{
    public interface IPlaceRepo : IRepositoryBase<Place>
    {
        IQueryable<Place> GetNearbyQueryable(double latitude, double longitude, double distanceKm);
    }
}
