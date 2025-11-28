using PartyRaidR.Shared.Models;
using PartyRaidR.Shared.Dtos.AuthenticationRequests;

namespace PartyRaidR.Shared.Converters
{
    public static class UserRegistrationConverter
    {
        public static UserRegistrationDto ConvertToUserRegistrationDto(this User user) =>
            new UserRegistrationDto()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                ProfilePictureUrl = user.ProfilePictureUrl,
                RegisterDate = user.RegisterDate,
                Role = user.Role,
                CityId = user.CityId,
                Password = user.PasswordHash
            };

        public static User ConvertToUser(this UserRegistrationDto userRegistrationDto) =>
            new User()
            {
                Id = userRegistrationDto.Id,
                Username = userRegistrationDto.Username,
                Email = userRegistrationDto.Email,
                ProfilePictureUrl = userRegistrationDto.ProfilePictureUrl,
                RegisterDate = userRegistrationDto.RegisterDate,
                Role = userRegistrationDto.Role,
                CityId = userRegistrationDto.CityId,
                PasswordHash = userRegistrationDto.Password
            };
    }
}
