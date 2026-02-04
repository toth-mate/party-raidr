using PartyRaidR.Backend.Converters;
using PartyRaidR.Backend.Models;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Assemblers
{
    public class CityAssembler : Assembler<City, CityDto>
    {
        public override CityDto ConvertToDto(City model) =>
            model.ConvertToCityDto();

        public override City ConvertToModel(CityDto dto) =>
            dto.ConvertToCity();
    }
}
