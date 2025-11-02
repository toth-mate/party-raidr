namespace PartyRaidR.Shared.Dtos
{
    public class UserRegistrationDto : UserDto
    {
        public string PasswordHash { get; set; } = string.Empty;
    }
}
