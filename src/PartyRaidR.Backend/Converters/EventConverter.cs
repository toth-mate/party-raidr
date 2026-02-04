using PartyRaidR.Backend.Models;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Converters
{
    public static class EventConverter
    {
        public static EventDto ConvertToEventDto(this Event @event) =>
            new EventDto()
            {
                Id = @event.Id,
                Title = @event.Title,
                Description = @event.Description,
                StartingDate = @event.StartingDate,
                EndingDate = @event.EndingDate,
                PlaceId = @event.PlaceId,
                Category = @event.Category,
                AuthorId = @event.AuthorId,
                Room = @event.Room,
                TicketPrice = @event.TicketPrice,
                DateCreated = @event.DateCreated
            };

        public static Event ConvertToEvent(this EventDto eventDto) =>
            new Event()
            {
                Id = eventDto.Id,
                Title = eventDto.Title,
                Description = eventDto.Description,
                StartingDate = eventDto.StartingDate,
                EndingDate = eventDto.EndingDate,
                PlaceId = eventDto.PlaceId,
                Category = eventDto.Category,
                AuthorId = eventDto.AuthorId,
                Room = eventDto.Room,
                TicketPrice = eventDto.TicketPrice,
                DateCreated = eventDto.DateCreated
            };
    }
}
