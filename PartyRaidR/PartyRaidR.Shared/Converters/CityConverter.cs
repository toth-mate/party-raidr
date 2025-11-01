using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Shared.Converters
{
    public static class CityConverter
    {
        public static CityDto ConvertToCityDto(this City city) =>
            new CityDto()
            {
                Id = city.Id,
                Name = city.Name,
                ZipCode = city.ZipCode,
                County = city.County,
                Country = city.Country
            };

        public static City ConvertToCity(this CityDto cityDto) =>
            new City()
            {
                Id = cityDto.Id,
                Name = cityDto.Name,
                ZipCode = cityDto.ZipCode,
                County = cityDto.County,
                Country = cityDto.Country
            };
    }
}
