using PartyRaidR.Shared.Converters;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Shared.Assemblers
{
    public class PlaceAssembler : Assembler<Place, PlaceDto>
    {
        public override PlaceDto ConvertToDto(Place model) =>
            model.ConvertToPlaceDto();

        public override Place ConvertToModel(PlaceDto dto) =>
            dto.ConvertToPlace();
    }
}
