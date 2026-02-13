using Microsoft.EntityFrameworkCore;
using PartyRaidR.Backend.Context;
using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Repos.Base;
using PartyRaidR.Backend.Repos.Promises;

namespace PartyRaidR.Backend.Repos
{
    public class CityRepo : RepositoryBase<City>, ICityRepo
    {
        public CityRepo(AppDbContext? context) : base(context)
        {
        }

        public async Task<IEnumerable<City>> GetTrendingCitiesAsync()
        {
            return await _context.Places
                .GroupBy(c => c.CityId)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => g.Key)
                .Join(_dbSet!,
                    cityId => cityId,
                    city => city.Id,
                    (cityId, city) => new City
                    {
                        Id = city.Id,
                        Name = city.Name,
                        ZipCode = city.ZipCode,
                        County = city.County,
                        Country = city.Country
                    }).ToListAsync();
        }
    }
}
