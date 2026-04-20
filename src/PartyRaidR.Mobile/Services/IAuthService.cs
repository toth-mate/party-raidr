using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Mobile.Services
{
    public interface IAuthService
    {
        Task Login(string email, string password);
        void Logout();
        UserDto? User { get; }
        bool IsLoggedIn { get; }
    }
}
