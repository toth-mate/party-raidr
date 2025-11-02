using PartyRaidR.Shared.Models;

namespace PartyRaidR.Shared.Dtos
{
    public class PlaceDto
    {
        public string Id { get; set; } = Guid.Empty.ToString();
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string CityId { get; set; } = string.Empty;
        public PlaceCategory Category { get; set; } = PlaceCategory.None;
        public string GpsLattitude { get; set; } = string.Empty;
        public string GpsLongitude { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
