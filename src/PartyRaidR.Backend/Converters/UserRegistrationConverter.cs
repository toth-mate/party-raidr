using PartyRaidR.Shared.Dtos.AuthenticationRequests;
using PartyRaidR.Backend.Models;

namespace PartyRaidR.Backend.Converters
{
    public static class UserRegistrationConverter
    {
        public static UserRegistrationDto ConvertToUserRegistrationDto(this User user) =>
            new UserRegistrationDto()
            {
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                Password = user.PasswordHash,
                BirthDate = user.BirthDate
            };

        public static User ConvertToUser(this UserRegistrationDto userRegistrationDto) =>
            new User()
            {
                Username = userRegistrationDto.Username,
                Email = userRegistrationDto.Email,
                Role = userRegistrationDto.Role,
                PasswordHash = userRegistrationDto.Password,
                BirthDate = userRegistrationDto.BirthDate
            };
    }
}
