using PartyRaidR.Backend.Converters;
using PartyRaidR.Backend.Models;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Assemblers
{
    public class EventAssembler : Assembler<Event, EventDto>
    {
        public override EventDto ConvertToDto(Event model) =>
            model.ConvertToEventDto();

        public override Event ConvertToModel(EventDto dto) =>
            dto.ConvertToEvent();
    }
}
