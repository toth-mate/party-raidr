using PartyRaidR.Backend.Context;
using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Repos.Base;
using PartyRaidR.Backend.Repos.Promises;

namespace PartyRaidR.Backend.Repos
{
    public class NotificationRepo : RepositoryBase<Notification>, INotificationRepo
    {
        public NotificationRepo(AppDbContext? context) : base(context)
        {
        }
    }
}
