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
                Location = place.Location,
                Description = place.Description
            };

        public static Place ConvertToPlace(this PlaceDto placeDto) =>
            new Place()
            {
                Id = placeDto.Id,
                Name = placeDto.Name,
                Address = placeDto.Address,
                CityId = placeDto.CityId,
                Category = placeDto.Category,
                Location = placeDto.Location,
                Description = placeDto.Description
            };
    }
}
