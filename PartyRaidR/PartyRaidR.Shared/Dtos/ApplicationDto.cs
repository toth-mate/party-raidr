using PartyRaidR.Shared.Models;

namespace PartyRaidR.Shared.Dtos
{
    public class ApplicationDto
    {
        public string Id { get; set; } = Guid.Empty.ToString();
        public string UserId { get; set; } = string.Empty;
        public string EventId { get; set; } = string.Empty;
        public DateTime TimeOfApplication { get; set; } = DateTime.Now;
        public StatusType Status { get; set; } = StatusType.NoApplicationNeeded;
    }
}
