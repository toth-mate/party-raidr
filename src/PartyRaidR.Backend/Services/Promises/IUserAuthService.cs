using PartyRaidR.Backend.Models.Responses;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Dtos.AuthenticationRequests;

namespace PartyRaidR.Backend.Services.Promises
{
    public interface IUserAuthService
    {
        Task<ServiceResponse<UserDto>> RegisterAsync(UserRegistrationDto user);
        Task<ServiceResponse<string>> LoginAsync(UserLoginDto user);
    }
}
