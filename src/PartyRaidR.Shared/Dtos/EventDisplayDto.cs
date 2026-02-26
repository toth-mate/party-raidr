using PartyRaidR.Shared.Enums;

namespace PartyRaidR.Shared.Dtos
{
    public class EventDisplayDto : IHasId
    {
        public string Id { get; set; } = Guid.Empty.ToString();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartingDate { get; set; } = DateTime.Now;
        public DateTime EndingDate { get; set; } = DateTime.Now;
        public string City { get; set; } = string.Empty;
        public string PlaceName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public int Room { get; set; } = 0;
        public decimal TicketPrice { get; set; } = 0;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public EventStatus EventStatus
        {
            get
            {
                var now = DateTime.UtcNow;

                if (EndingDate < now)
                    return EventStatus.Past;

                if (StartingDate <= now && EndingDate >= now)
                    return EventStatus.Live;

                if (StartingDate > now && StartingDate < now.AddHours(3))
                    return EventStatus.StartingSoon;

                return EventStatus.Upcoming;
            }
        }
    }
}
