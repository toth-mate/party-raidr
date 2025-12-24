using Microsoft.EntityFrameworkCore;
using PartyRaidR.Backend.Context;
using PartyRaidR.Backend.Repos.Base;
using PartyRaidR.Backend.Repos.Promises;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Backend.Repos
{
    public class UserRepo : RepositoryBase<User>, IUserRepo
    {
        public UserRepo(AppDbContext? context) : base(context)
        {
        }

        public async Task<bool> EmailExistsAsync(string email) =>
            await _dbSet!.AnyAsync(u => u.Email == email);

        public async Task<User?> GetByEmailAsync(string email) =>
            await _dbSet!.FirstOrDefaultAsync(u => u.Email == email);
    }
}
