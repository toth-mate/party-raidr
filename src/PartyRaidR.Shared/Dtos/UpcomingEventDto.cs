namespace PartyRaidR.Shared.Dtos
{
    public class UpcomingEventDto : IHasId
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string PlaceName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; } = DateTime.Now;
    }
}
