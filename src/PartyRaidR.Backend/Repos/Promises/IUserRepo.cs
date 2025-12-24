using PartyRaidR.Backend.Repos.Base;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Backend.Repos.Promises
{
    public interface IUserRepo : IRepositoryBase<User>
    {
        Task<bool> EmailExistsAsync(string email);
        Task<User?> GetByEmailAsync(string email);
    }
}
