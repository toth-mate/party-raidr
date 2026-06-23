using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Models.Responses;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Services.Promises
{
    public interface ICityService : IBaseService<City, CityDto>
    {
        /// <summary>
        /// Get the number of places in a city.
        /// </summary>
        /// <param name="id">The unique identifier of the city</param>
        /// <returns>An integer representing the number of places in the citiy</returns>
        Task<ServiceResponse<int>> GetNumberOfPlacesAsync(string id);

        /// <summary>
        /// Get a list of cities in a county.
        /// </summary>
        /// <param name="county">The county to look for cities in</param>
        /// <returns>A list of cities in the county</returns>
        Task<ServiceResponse<IEnumerable<CityDto>>> GetByCountyAsync(string county);

        /// <summary>
        /// Get a list of trending cities.
        /// "Trending" cities are the top five cities with the most places.
        /// </summary>
        /// <returns>A list of cities</returns>
        Task<ServiceResponse<List<CityDto>>> GetTrendingCitiesAsync();
    }
}
