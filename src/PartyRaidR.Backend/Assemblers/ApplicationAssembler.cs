using PartyRaidR.Backend.Converters;
using PartyRaidR.Backend.Models;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Assemblers
{
    public class ApplicationAssembler : Assembler<Application, ApplicationDto>
    {
        public override ApplicationDto ConvertToDto(Application model) =>
            model.ConvertToApplicationDto();

        public override Application ConvertToModel(ApplicationDto dto) =>
            dto.ConvertToApplication();
    }
}
