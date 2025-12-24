using PartyRaidR.Shared.Models;

namespace PartyRaidR.Shared.Dtos
{
    public class NotificationDto
    {
        public string Id { get; set; } = Guid.Empty.ToString();
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string EventId { get; set; } = string.Empty;
        public NotificationType Type { get; set; } = NotificationType.General;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
    }
}
