using NetTopologySuite.Geometries;
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

        public IQueryable<Place> GetNearbyQueryable(double latitude, double longitude, double distanceKm)
        {
            // User location
            Point location = new Point(longitude, latitude) { SRID = 4326 };
            return _dbSet!.Where(p => p.Location.IsWithinDistance(location, distanceKm * 1000));
        }
    }
}
