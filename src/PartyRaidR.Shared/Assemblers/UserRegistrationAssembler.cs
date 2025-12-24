using PartyRaidR.Shared.Models;
using PartyRaidR.Shared.Converters;
using PartyRaidR.Shared.Dtos.AuthenticationRequests;

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
