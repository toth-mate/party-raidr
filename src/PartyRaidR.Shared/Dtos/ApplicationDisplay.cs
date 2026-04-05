namespace PartyRaidR.Shared.Dtos
{
    public class ApplicationDisplay : IHasId
    {
        // Application ID
        public string Id { get; set; } = Guid.Empty.ToString();
        public string EventId { get; set; } = Guid.Empty.ToString();
        public string Title { get; set; } = string.Empty;
        public DateTime DateOfApplication { get; set; } = DateTime.Now;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = string.Empty;
    }
}