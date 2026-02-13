using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Repos.Base;

namespace PartyRaidR.Backend.Repos.Promises
{
    public interface ICityRepo : IRepositoryBase<City>
    {
        Task<IEnumerable<City>> GetTrendingCitiesAsync();
    }
}
