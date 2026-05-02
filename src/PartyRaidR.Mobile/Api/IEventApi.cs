using Refit;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Mobile.Api
{
    public interface IEventApi
    {
        [Get("/event/display-all")]
        Task<List<EventDisplayDto>> DisplayAll();

        [Get("/event/display/{id}")]
        Task<EventDisplayDto> GetEventDisplay(string id);

        [Post("/event")]
        Task<object> CreateEvent([Body] EventDto newEvent);
    }
}
