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
        private readonly ICityRepo _cityRepo;
        private readonly IPlaceRepo _placeRepo;
        private readonly IUserService _userService;

        public CityService(CityAssembler assembler, ICityRepo? repo, IUserContext userContext, IPlaceRepo placeRepo, IUserService userService) : base(assembler, repo, userContext)
        {
            _cityRepo = repo!;
            _placeRepo = placeRepo;
            _userService = userService;
        }

        public async Task<ServiceResponse<IEnumerable<CityDto>>> GetByCountyAsync(string county)
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
            catch (Exception ex)
            {
                return new ServiceResponse<IEnumerable<CityDto>>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = $"An error occurred while retrieving cities for county '{county}': {ex.Message}."
                };
            }
        }

        public async Task<ServiceResponse<int>> GetNumberOfPlacesAsync(string id)
        {
            try
            {
                int count = _placeRepo.GetAllAsQueryable().Count(p => p.CityId == id);

                return new ServiceResponse<int>
                {
                    Success = true,
                    StatusCode = 200,
                    Data = count
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = $"An error occurred while retrieving the number of places for this city: {ex.Message}."
                };
            }
        }

        public async Task<ServiceResponse<List<CityDto>>> GetTrendingCitiesAsync()
        {
            try
            {
                var trendingCities = await _cityRepo.GetTrendingCitiesAsync();
                List<CityDto> result = trendingCities.Select(_assembler.ConvertToDto).ToList();

                return new ServiceResponse<List<CityDto>>
                {
                    Data = result,
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<CityDto>>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = $"An error occurred while retrieving trending cities: {ex.Message}."
                };
            }
        }

        public async override Task<ServiceResponse<CityDto>> AddAsync(CityDto dto)
        {
            bool isUserAdmin = await IsUserAdmin();

            if (!isUserAdmin)
            {
                return new ServiceResponse<CityDto>
                {
                    Success = false,
                    StatusCode = 403,
                    Message = "Your account lacks permission to complete this action."
                };
            }

            return await base.AddAsync(dto);
        }

        public async override Task<ServiceResponse<CityDto>> DeleteAsync(string id)
        {
            bool isUserAdmin = await IsUserAdmin();

            if (!isUserAdmin)
            {
                return new ServiceResponse<CityDto>
                {
                    Success = false,
                    StatusCode = 403,
                    Message = "Your account lacks permission to complete this action."
                };
            }

            return await base.DeleteAsync(id);
        }

        public async override Task<ServiceResponse<CityDto>> UpdateAsync(CityDto dto)
        {
            bool isUserAdmin = await IsUserAdmin();

            if (!isUserAdmin)
            {
                return new ServiceResponse<CityDto>
                {
                    Success = false,
                    StatusCode = 403,
                    Message = "Your account lacks permission to complete this action."
                };
            }

            return await base.UpdateAsync(dto);
        }

        private async Task<bool> IsUserAdmin()
        {
            var userResult = await _userService.GetByIdAsync(_userContext.UserId);
            return userResult.Success && userResult.Data is not null && userResult.Data.Role == UserRole.Admin;
        }
    }
}
