namespace PartyRaidR.Shared.Models
{
    public enum NotificationType
    {
        General = 0,
        Info = 1,
        Warning = 2
    }

    public class Notification : IDbEntity<Notification>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public string EventId { get; set; }
        public NotificationType Type { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsRead { get; set; }

        public override string ToString() =>
            $"{Title} | {Text}";
    }
}
