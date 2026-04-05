namespace PartyRaidR.Shared.Dtos
{
    public class ApplicationDisplayDto : IHasId
    {
        // Application ID
        public string Id { get; set; } = Guid.Empty.ToString();
        public string EventId { get; set; } = Guid.Empty.ToString();
        public string Title { get; set; } = string.Empty;
        public string DateOfApplication { get; set; } = DateTime.Now.ToString("g");
        public string StartDate { get; set; } = DateTime.Now.ToString("g");
        public string EndDate { get; set; } = DateTime.Now.ToString("g");
        public string Status { get; set; } = string.Empty;
    }
}