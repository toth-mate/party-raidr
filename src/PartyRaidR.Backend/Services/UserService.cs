using PartyRaidR.Backend.Assemblers;
using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Repos.Promises;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Services
{
    public class UserService : BaseService<User, UserDto>, IUserService
    {
        public UserService(UserAssembler? assembler, IUserRepo? repo, IUserContext? userContext) : base(assembler, repo, userContext)
        {
        }
    }
}
