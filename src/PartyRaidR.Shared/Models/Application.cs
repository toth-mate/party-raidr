namespace PartyRaidR.Shared.Models
{
    public enum StatusType
    {
        NoApplicationNeeded = 0,
        Pending = 1,
        Accepted = 2,
        Rejected = 3,
        WaitList = 4
    }

    public class Application : IDbEntity<Application>
    {
        public Application()
        {
            Id = Guid.Empty.ToString();
            UserId = string.Empty;
            EventId = string.Empty;
            TimeOfApplication = DateTime.Now;
            Status = StatusType.NoApplicationNeeded;
        }

        public Application(Guid id, string userId, string eventId, DateTime timeOfApplication, StatusType status)
        {
            Id = Guid.Empty.ToString();
            UserId = userId;
            EventId = eventId;
            TimeOfApplication = timeOfApplication;
            Status = status;
        }

        public string Id { get; set; }
        public string UserId { get; set; }
        public string EventId { get; set; }
        public DateTime TimeOfApplication { get; set; }
        public StatusType Status { get; set; }

        public User User { get; set; }
        public Event Event { get; set; }
    }
}
