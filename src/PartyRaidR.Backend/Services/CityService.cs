using Microsoft.EntityFrameworkCore;
using PartyRaidR.Backend.Repos.Base;
using PartyRaidR.Backend.Repos.Promises;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Assemblers;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;
using PartyRaidR.Shared.Models.Responses;

namespace PartyRaidR.Backend.Services
{
    public class CityService : BaseService<City, CityDto>, ICityService
    {
        public CityService(CityAssembler assembler, ICityRepo? repo, IUserContext userContext) : base(assembler, repo, userContext)
        {
        }

        public async Task<ServiceResponse<IEnumerable<CityDto>>> GetByCounty(string county)
        {
            try
            {
                var cities = await _repo.FindByConditionAsync(c => c.County == county);
                List<CityDto> cityDtos = cities.Select(_assembler.ConvertToDto).ToList();

                return new ServiceResponse<IEnumerable<CityDto>>
                {
                    Success = true,
                    StatusCode = 200,
                    Data = cityDtos,
                    Message = cityDtos.Count == 0
                            ? $"No cities found in county '{county}'"
                            : string.Empty
                };
            }
            catch(Exception ex)
            {
                return new ServiceResponse<IEnumerable<CityDto>>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = $"An error occurred while retrieving cities for county '{county}': {ex.Message}."
                };
            }
        }

        public async Task<ServiceResponse<int>> GetNumberOfPlaces(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<CityDto>>> GetPopularCities()
        {
            throw new NotImplementedException();
        }
    }
}
