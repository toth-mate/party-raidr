using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Shared.Converters
{
    public static class UserConverter
    {
        public static UserDto ConvertToUserDto(this User user) =>
            new UserDto()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                ProfilePictureUrl = user.ProfilePictureUrl,
                RegisterDate = user.RegisterDate,
                Role = user.Role,
                CityId = user.CityId
            };

        public static User ConvertToUser(this UserDto userDto) =>
            new User()
            {
                Id = userDto.Id,
                Username = userDto.Username,
                Email = userDto.Email,
                ProfilePictureUrl = userDto.ProfilePictureUrl,
                RegisterDate = userDto.RegisterDate,
                Role = userDto.Role,
                CityId = userDto.CityId
            };
    }
}
