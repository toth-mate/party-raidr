using PartyRaidR.Shared.Models;

namespace PartyRaidR.Shared.Dtos.AuthenticationRequests
{
    public class UserRegistrationDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.User;
        public string Password { get; set; } = string.Empty;
    }
}
