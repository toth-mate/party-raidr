using PartyRaidR.Backend.Converters;
using PartyRaidR.Backend.Models;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Assemblers
{
    public class PlaceAssembler : Assembler<Place, PlaceDto>
    {
        public override PlaceDto ConvertToDto(Place model) =>
            model.ConvertToPlaceDto();

        public override Place ConvertToModel(PlaceDto dto) =>
            dto.ConvertToPlace();
    }
}
