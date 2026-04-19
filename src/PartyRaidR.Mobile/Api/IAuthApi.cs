using Refit;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Dtos.AuthenticationRequests;

namespace PartyRaidR.Mobile.Api
{
    public interface IAuthApi
    {
        [Post("/auth/login")]
        Task<string> Login([Body] UserLoginDto credentials);
    }
}
