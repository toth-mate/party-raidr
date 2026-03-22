using Refit;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Mobile.Api
{
    public interface IEventApi
    {
        [Get("/event/display-all")]
        Task<List<EventDisplayDto>> DisplayAll();
    }
}
