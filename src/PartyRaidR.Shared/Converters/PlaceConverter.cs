using NetTopologySuite.Geometries;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Shared.Converters
{
    public static class PlaceConverter
    {
        public static PlaceDto ConvertToPlaceDto(this Place place) =>
            new PlaceDto()
            {
                Id = place.Id,
                Name = place.Name,
                Address = place.Address,
                CityId = place.CityId,
                Category = place.Category,
                Longitude = place.Location.X,
                Latitude = place.Location.Y,
                Description = place.Description,
                UserId = place.UserId
            };

        public static Place ConvertToPlace(this PlaceDto placeDto) =>
            new Place()
            {
                Id = placeDto.Id,
                Name = placeDto.Name,
                Address = placeDto.Address,
                CityId = placeDto.CityId,
                Category = placeDto.Category,
                Location = new Point(placeDto.Longitude, placeDto.Latitude),
                Description = placeDto.Description,
                UserId = placeDto.UserId
            };
    }
}
