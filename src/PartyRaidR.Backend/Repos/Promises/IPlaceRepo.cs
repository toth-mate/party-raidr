using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Repos.Base;

namespace PartyRaidR.Backend.Repos.Promises
{
    public interface IPlaceRepo : IRepositoryBase<Place>
    {
        IQueryable<Place> GetNearbyQueryable(double latitude, double longitude, double distanceKm);
    }
}
