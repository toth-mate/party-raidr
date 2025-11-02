using PartyRaidR.Shared.Converters;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Shared.Assemblers
{
    public class CityAssembler : Assembler<City, CityDto>
    {
        public override CityDto ConvertToDto(City model) =>
            model.ConvertToCityDto();

        public override City ConvertToModel(CityDto dto) =>
            dto.ConvertToCity();
    }
}
