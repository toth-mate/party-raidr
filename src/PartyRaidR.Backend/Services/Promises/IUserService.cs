using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Backend.Services.Promises
{
    public interface IUserService : IBaseService<User, UserDto>
    {
    }
}
