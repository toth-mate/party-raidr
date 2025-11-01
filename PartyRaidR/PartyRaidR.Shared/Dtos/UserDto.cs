using PartyRaidR.Shared.Models;

namespace PartyRaidR.Shared.Dtos
{
    public class UserDto
    {
        public string Id { get; set; } = Guid.Empty.ToString();
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public UserRole Role { get; set; } = UserRole.User;
        public string CityId { get; set; } = string.Empty;
    }
}
