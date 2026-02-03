using PartyRaidR.Shared.Models;

namespace PartyRaidR.Shared.Dtos
{
    public class UserDto : IHasId
    {
        public string Id { get; set; } = Guid.Empty.ToString();
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ProfilePictureUrl { get; set; } = string.Empty;
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public DateOnly BirthDate { get; set; } = new DateOnly(2000, 1, 1);
        public UserRole Role { get; set; } = UserRole.User;
    }
}
