using PartyRaidR.Shared.Enums;

namespace PartyRaidR.Shared.Dtos.AuthenticationRequests
{
    public class UserRegistrationDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.User;
        public DateOnly BirthDate { get; set; } = new DateOnly(2000, 1, 1);
        public string Password { get; set; } = string.Empty;
    }
}
