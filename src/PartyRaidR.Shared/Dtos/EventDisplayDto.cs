using PartyRaidR.Shared.Enums;

namespace PartyRaidR.Shared.Dtos
{
    public class EventDisplayDto : IHasId
    {
        public string Id { get; set; } = Guid.Empty.ToString();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string StartingDate { get; set; } = DateTime.Now.ToShortDateString();
        public string EndingDate { get; set; } = DateTime.Now.ToShortDateString();
        public string City { get; set; } = string.Empty;
        public string PlaceName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public int Room { get; set; } = 0;
        public decimal TicketPrice { get; set; } = 0;
        public string DateCreated { get; set; } = DateTime.Now.ToShortDateString();
        public bool IsActive { get; set; } = true;
        public string EventStatus { get; set; } = string.Empty;
    }
}
