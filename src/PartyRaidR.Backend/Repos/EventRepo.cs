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

        public async Task<List<Event>> FilterEventsAsync(string? title,
                                                    string? description,
                                                    DateTime? startingDate,
                                                    DateTime? endingDate,
                                                    string? placeId,
                                                    string? cityId,
                                                    EventCategory? category,
                                                    decimal? ticketPriceMin,
                                                    decimal? ticketPriceMax)
        {
            var result = GetAllAsQueryable();

            if(title is not null)
                result = result.Where(e => e.Title.Contains(title));

            if (description is not null)
                result = result.Where(e => e.Description.Contains(description));

            if(startingDate is not null)
                result = result.Where(e => e.StartingDate >= startingDate);

            if(endingDate is not null)
                result = result.Where(e => e.EndingDate <= endingDate);

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

            return await result.ToListAsync();
        }

        public async Task<List<Event>> GetEventsByUserIdAsync(string userId) =>
            await _dbSet!.Include(e => e.Place)
                         .Where(e => e.User.Id == userId)
                         .ToListAsync();
    }
}
