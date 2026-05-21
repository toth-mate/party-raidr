using PartyRaidR.Shared.Dtos;
using Refit;

namespace PartyRaidR.Mobile.Api
{
    public interface IPlaceApi
    {
        [Get("/place")]
        Task<List<PlaceDto>> GetPlaces();
    }
}
