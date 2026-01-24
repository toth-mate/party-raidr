using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;
using PartyRaidR.Shared.Models.Responses;

namespace PartyRaidR.Backend.Services.Promises
{
    public interface IPlaceService : IBaseService<Place, PlaceDto>
    {
        Task<ServiceResponse<IEnumerable<PlaceDto>>> FilterPlacesAsync(PlaceFilterDto filter);
        Task<ServiceResponse<IEnumerable<PlaceDto>>> GetMyPlacesAsync();
    }
}
