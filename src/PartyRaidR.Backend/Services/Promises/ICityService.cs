using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;
using PartyRaidR.Shared.Models.Responses;

namespace PartyRaidR.Backend.Services.Promises
{
    public interface ICityService : IBaseService<City, CityDto>
    {
        Task<ServiceResponse<int>> GetNumberOfPlaces(string id);
        Task<ServiceResponse<IEnumerable<CityDto>>> GetByCounty(string county);
        Task<ServiceResponse<List<CityDto>>> GetTrendingCitiesAsync();
    }
}
