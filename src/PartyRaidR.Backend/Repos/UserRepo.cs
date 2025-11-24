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
    }
}
