using PartyRaidR.Shared.Enums;

namespace PartyRaidR.Shared.Dtos
{
    public class PlaceDto : IHasId
    {
        public string Id { get; set; } = Guid.Empty.ToString();
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string CityId { get; set; } = string.Empty;
        public PlaceCategory Category { get; set; } = PlaceCategory.None;
        public double Latitude { get; set; } = 0f;
        public double Longitude { get; set; } = 0f;
        public string Description { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
