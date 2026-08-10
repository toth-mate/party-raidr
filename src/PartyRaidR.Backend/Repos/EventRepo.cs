using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PartyRaidR.Backend.Context;
using PartyRaidR.Backend.Exceptions;
using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Repos.Base;
using PartyRaidR.Backend.Repos.Promises;
using PartyRaidR.Shared.Enums;

namespace PartyRaidR.Backend.Repos
{
    public class EventRepo : RepositoryBase<Event>, IEventRepo
    {
        public EventRepo(AppDbContext? context) : base(context)
        {
        }

        public async Task<Event> GetEventWithDisplayData(string id) =>
            await _dbSet!.Include(e => e.Place)
                         .ThenInclude(p => p.City)
                         .Include(e => e.User)
                         .FirstOrDefaultAsync(e => e.Id == id) ?? throw new EntityNotFoundException("Event not found.");

        public async Task<List<Event>> GetEventsWithDisplayData() =>
            await _dbSet!.Include(e => e.Place)
                         .ThenInclude(p => p.City)
                         .Include(e => e.User)
                         .Where(e => e.IsActive && e.StartingDate >= DateTime.UtcNow)
                         .OrderBy(e => e.StartingDate)
                         .ToListAsync();
 
        public async Task<List<Event>> FilterEventsAsync(string? title,
                                                    string? description,
                                                    DateTime? startingDate,
                                                    DateTime? endingDate,
                                                    string? placeName,
                                                    string? placeId,
                                                    string? cityId,
                                                    EventCategory? category,
                                                    decimal? ticketPriceMin,
                                                    decimal? ticketPriceMax)
        {
            var result = GetAllAsQueryable().Where(e => e.IsActive && e.StartingDate >= DateTime.Now && e.EndingDate > DateTime.Now);

            if(title is not null)
                result = result.Where(e => e.Title.Contains(title));

            if (description is not null)
                result = result.Where(e => e.Description.Contains(description));

            if(startingDate is not null)
                result = result.Where(e => e.StartingDate >= startingDate);

            if(endingDate is not null)
                result = result.Where(e => e.EndingDate <= endingDate);

            if (placeName is not null)
                result = result.Include(e => e.Place).Where(e => e.Place.Name.Contains(placeName));

            if (placeId is not null)
                result = result.Include(e => e.Place).Where(e => e.Place.Id == placeId);

            if (cityId is not null)
                result = result.Include(e => e.Place).Include(e => e.Place.City).Where(e => e.Place.City.Id == cityId);

            if (category is not null)
                result = result.Where(e => e.Category == category);

            if (ticketPriceMin is not null)
                result = result.Where(e => e.TicketPrice >= ticketPriceMin);

            if (ticketPriceMax is not null)
                result = result.Where(e => e.TicketPrice <= ticketPriceMax);

            return await result.Include(e => e.User)
                .Include(e => e.Place)
                .ThenInclude(p => p.City)
                .OrderBy(e => e.StartingDate)
                .ToListAsync();
        }

        public async Task<List<Event>> GetEventsByUserIdAsync(string userId) =>
            await _dbSet!.Include(e => e.Place)
                         .Where(e => e.User.Id == userId)
                         .ToListAsync();

        public IQueryable<Event> GetEventsWithMarkerDetails(double minLat, double maxLat, double minLng, double maxLng)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var envelope = new Envelope(minLat, maxLat, minLng, maxLng);
            var polygon = geometryFactory.ToGeometry(envelope);
            
            return _dbSet!.Include(e => e.Place)
                .Where(e => e.Place.Location.Within(polygon))
                .Where(e => e.IsActive && e.StartingDate >= DateTime.UtcNow);
        }

        public async Task<List<Event>> GetNearbyEventsAsync(double latitude, double longitude, double radius)
        {
            IQueryable<Event> events = _dbSet!.Include(e => e.Place)
                .ThenInclude(p => p.City)
                .Where(e => e.IsActive && e.StartingDate >= DateTime.UtcNow)
                .OrderBy(e => e.StartingDate);
            
            Point location = new Point(longitude, latitude) { SRID = 4326 };
            return await events.Where(e => e.Place.Location.IsWithinDistance(location, radius * 1000)).ToListAsync();
        }
    }
}
