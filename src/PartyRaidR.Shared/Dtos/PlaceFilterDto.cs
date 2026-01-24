using PartyRaidR.Shared.Models;
using System.Drawing;

namespace PartyRaidR.Shared.Dtos
{
    public class PlaceFilterDto
    {
        public string? Name { get; set; } = null;
        public string? CityId { get; set; } = null;
        public PlaceCategory? Category { get; set; } = null;
        public double? MaxDistanceKm { get; set; } = null;
        public double Latitude { get; set; } = 0f;
        public double Longitude { get; set; } = 0f;
    }
}
