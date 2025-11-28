namespace PartyRaidR.Shared.Dtos.AuthenticationRequests
{
    public class UserRegistrationDto : UserDto
    {
        public string Password { get; set; } = string.Empty;
    }
}
