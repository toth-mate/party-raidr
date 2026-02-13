using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Repos.Base;

namespace PartyRaidR.Backend.Repos.Promises
{
    public interface IUserRepo : IRepositoryBase<User>
    {
        Task<bool> EmailExistsAsync(string email);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);
    }
}
