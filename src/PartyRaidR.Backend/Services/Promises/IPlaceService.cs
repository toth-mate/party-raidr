using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Models.Responses;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Services.Promises
{
    public interface IPlaceService : IBaseService<Place, PlaceDto>
    {
        /// <summary>
        /// Get a list of places based on a filter.
        /// </summary>
        /// <param name="filter">The object containing the filtering parameters</param>
        /// <returns>The list of places meeting the conditions provided</returns>
        Task<ServiceResponse<IEnumerable<PlaceDto>>> FilterPlacesAsync(PlaceFilterDto filter);

        /// <summary>
        /// Get places created by an authenticated user. The user is read from the token in the request header.
        /// </summary>
        /// <returns>A list of places created by the user</returns>
        Task<ServiceResponse<IEnumerable<PlaceDto>>> GetMyPlacesAsync();
    }
}
