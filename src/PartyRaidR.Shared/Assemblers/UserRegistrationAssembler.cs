using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;
using PartyRaidR.Shared.Converters;

namespace PartyRaidR.Shared.Assemblers
{
    public class UserRegistrationAssembler : Assembler<User, UserRegistrationDto>
    {
        public override UserRegistrationDto ConvertToDto(User model) =>
            model.ConvertToUserRegistrationDto();

        public override User ConvertToModel(UserRegistrationDto dto) =>
            dto.ConvertToUser();
    }
}
