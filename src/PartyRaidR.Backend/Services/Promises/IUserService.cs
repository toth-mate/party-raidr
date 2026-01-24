using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Shared.Models;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Services.Promises
{
    public interface IUserService : IBaseService<User, UserDto>
    {
    }
}
