using PartyRaidR.Backend.Converters;
using PartyRaidR.Backend.Models;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Assemblers
{
    public class UserAssembler : Assembler<User, UserDto>
    {
        public override UserDto ConvertToDto(User model) =>
            model.ConvertToUserDto();

        public override User ConvertToModel(UserDto dto) =>
            dto.ConvertToUser();
    }
}
