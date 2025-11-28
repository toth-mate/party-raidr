using PartyRaidR.Backend.Repos;
using PartyRaidR.Backend.Repos.Promises;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Assemblers;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Dtos.AuthenticationRequests;
using PartyRaidR.Shared.Models.Responses;

namespace PartyRaidR.Backend.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepo _userRepo;
        private readonly UserAssembler _userAssembler;

        public AuthenticationService(IUserRepo userRepo, UserAssembler userAssembler)
        {
            _userRepo = userRepo;
            _userAssembler = userAssembler;
        }

        public Task<ServiceResponse<string>> LoginAsync(UserLoginDto user)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<UserDto>> RegisterAsync(UserRegistrationDto user)
        {
            throw new NotImplementedException();
        }
    }
}
