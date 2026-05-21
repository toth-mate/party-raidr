namespace PartyRaidR.Shared.Dtos
{
    public class CreateEventDto : IHasId
    {
        public string Id { get; set; } = Guid.Empty.ToString();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartingDate { get; set; } = DateTime.Now;
        public DateTime EndingDate { get; set; } = DateTime.Now;
        public string PlaceId { get; set; } = Guid.Empty.ToString();
        public string Category { get; set; } = string.Empty;
        public int Room { get; set; } = 0;
        public decimal TicketPrice { get; set; } = 0;
    }
}
