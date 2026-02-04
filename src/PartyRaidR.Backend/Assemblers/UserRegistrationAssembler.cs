using PartyRaidR.Backend.Converters;
using PartyRaidR.Backend.Models;
using PartyRaidR.Shared.Dtos.AuthenticationRequests;

namespace PartyRaidR.Backend.Assemblers
{
    public class UserRegistrationAssembler : Assembler<User, UserRegistrationDto>
    {
        public override UserRegistrationDto ConvertToDto(User model) =>
            model.ConvertToUserRegistrationDto();

        public override User ConvertToModel(UserRegistrationDto dto) =>
            dto.ConvertToUser();
    }
}
