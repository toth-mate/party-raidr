using PartyRaidR.Shared.Models;
using PartyRaidR.Shared.Dtos.AuthenticationRequests;

namespace PartyRaidR.Shared.Converters
{
    public static class UserRegistrationConverter
    {
        public static UserRegistrationDto ConvertToUserRegistrationDto(this User user) =>
            new UserRegistrationDto()
            {
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                Password = user.PasswordHash
            };

        public static User ConvertToUser(this UserRegistrationDto userRegistrationDto) =>
            new User()
            {
                Username = userRegistrationDto.Username,
                Email = userRegistrationDto.Email,
                Role = userRegistrationDto.Role,
                PasswordHash = userRegistrationDto.Password
            };
    }
}
