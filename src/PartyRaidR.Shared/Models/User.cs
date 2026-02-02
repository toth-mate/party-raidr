namespace PartyRaidR.Shared.Models
{
    public enum UserRole
    {
        User = 0,
        Admin = 1
    }

    public class User : IDbEntity<User>
    {
        public User()
        {
            Id = Guid.Empty.ToString();
            Username = string.Empty;
            Email = string.Empty;
            PasswordHash = string.Empty;
            ProfilePictureUrl = string.Empty;
            RegisterDate = DateTime.Now;
            BirthDate = new DateOnly(RegisterDate.Year, RegisterDate.Month, RegisterDate.Day);
            Role = UserRole.User;
            CityId = string.Empty;
        }

        public User(Guid id, string username, string email, string passwordHash, string profilePictureUrl, DateTime registerDate, DateOnly birthDate, UserRole role, string cityId)
        {
            Id = id.ToString();
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            ProfilePictureUrl = profilePictureUrl;
            RegisterDate = registerDate;
            BirthDate = birthDate;
            Role = role;
            CityId = cityId;
        }

        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string ProfilePictureUrl { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateOnly BirthDate { get; set; }
        public UserRole Role { get; set; }
        public string CityId { get; set; }

        public City City { get; set; }
        public List<Place> Places { get; set; } = new();
        public List<Notification> Notifications { get; set; } = new();
        public List<Event> Events { get; set; } = new();
        public List<Application> Applications { get; set; } = new();

        public override string ToString() =>
            $"{Username}";
    }
}
