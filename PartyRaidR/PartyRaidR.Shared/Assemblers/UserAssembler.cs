using PartyRaidR.Shared.Converters;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Shared.Assemblers
{
    public class UserAssembler : Assembler<User, UserDto>
    {
        public override UserDto ConvertToDto(User model) =>
            model.ConvertToUserDto();

        public override User ConvertToModel(UserDto dto) =>
            dto.ConvertToUser();
    }
}
