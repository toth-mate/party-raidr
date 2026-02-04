using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Models.Responses;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Services.Promises
{
    public interface IPlaceService : IBaseService<Place, PlaceDto>
    {
        Task<ServiceResponse<IEnumerable<PlaceDto>>> FilterPlacesAsync(PlaceFilterDto filter);
        Task<ServiceResponse<IEnumerable<PlaceDto>>> GetMyPlacesAsync();
    }
}
