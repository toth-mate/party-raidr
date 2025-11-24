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
        public Notification()
        {
            Id = Guid.Empty.ToString();
            Title = string.Empty;
            Text = string.Empty;
            UserId = string.Empty;
            EventId = string.Empty;
            Type = NotificationType.General;
            DateCreated = DateTime.Now;
            IsRead = false;
        }

        public Notification(Guid id, string title, string text, string userId, string eventId, NotificationType type, DateTime dateCreated, bool isRead)
        {
            Id = id.ToString();
            Title = title;
            Text = text;
            UserId = userId;
            EventId = eventId;
            Type = type;
            DateCreated = dateCreated;
            IsRead = isRead;
        }

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
