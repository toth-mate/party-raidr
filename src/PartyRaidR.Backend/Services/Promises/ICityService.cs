using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Models.Responses;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Services.Promises
{
    public interface ICityService : IBaseService<City, CityDto>
    {
        Task<ServiceResponse<int>> GetNumberOfPlacesAsync(string id);
        Task<ServiceResponse<IEnumerable<CityDto>>> GetByCountyAsync(string county);
        Task<ServiceResponse<List<CityDto>>> GetTrendingCitiesAsync();
    }
}
