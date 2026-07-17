using PartyRaidR.Shared.Enums;
using System.Text.Json.Serialization;

namespace PartyRaidR.Shared.Dtos
{
    public class EventMarkerDto : IHasId
    {
        public string Id { get; set; } = Guid.Empty.ToString();
        public string Title { get; set; } = string.Empty;
        public DateTime StartingDate { get; set; } = DateTime.Now;
        public DateTime EndingDate { get; set; } = DateTime.Now;
        public string Address { get; set; } = string.Empty;
        public double Latitude { get; set; } = 0f;
        public double Longitude { get; set; } = 0f;
    }
}
