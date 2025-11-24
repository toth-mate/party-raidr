using PartyRaidR.Shared.Converters;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Shared.Assemblers
{
    public class EventAssembler : Assembler<Event, EventDto>
    {
        public override EventDto ConvertToDto(Event model) =>
            model.ConvertToEventDto();

        public override Event ConvertToModel(EventDto dto) =>
            dto.ConvertToEvent();
    }
}
