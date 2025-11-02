using PartyRaidR.Shared.Converters;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Shared.Assemblers
{
    public class ApplicationAssembler : Assembler<Application, ApplicationDto>
    {
        public override ApplicationDto ConvertToDto(Application model) =>
            model.ConvertToApplicationDto();

        public override Application ConvertToModel(ApplicationDto dto) =>
            dto.ConvertToApplication();
    }
}
