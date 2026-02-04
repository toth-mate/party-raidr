using PartyRaidR.Shared.Enums;

namespace PartyRaidR.Shared.Dtos
{
    public class EventFilterDto
    {
        public string? Title { get; set; } = null;
        public string? Description { get; set; } = null;
        public DateTime? StartingDate { get; set; } = null;
        public DateTime? EndingDate { get; set; } = null;
        public string? PlaceId { get; set; } = null;
        public string? CityId { get; set; } = null;
        public EventCategory? Category { get; set; } = EventCategory.None;
        public decimal? TicketPriceMin { get; set; } = null;
        public decimal? TicketPriceMax { get; set; } = null;
    }
}
