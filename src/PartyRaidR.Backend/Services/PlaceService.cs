using Microsoft.EntityFrameworkCore;
using PartyRaidR.Backend.Repos;
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
    public class PlaceService : BaseService<Place, PlaceDto>, IPlaceService
    {
        private readonly IPlaceRepo _placeRepo;
        private readonly IUserService _userService;

        public PlaceService(PlaceAssembler? assembler, IPlaceRepo? repo, IUserContext userContext, IUserService userService) : base(assembler, repo, userContext)
        {
            _placeRepo = repo!;
            _userService = userService;
        }

        public override async Task<ServiceResponse<PlaceDto>> UpdateAsync(PlaceDto dto)
        {
            string userId = _userContext.UserId;

            var userResult = await _userService.GetByIdAsync(userId);

            Place? place = await _repo.GetByIdAsync(dto.Id);

            if (place is not null)
            {
                if (userResult.Success && userResult.Data is not null)
                {
                    if (userResult.Data.Role != UserRole.Admin || dto.UserId != userId)
                    {
                        return new ServiceResponse<PlaceDto>
                        {
                            Success = false,
                            StatusCode = 403
                        };
                    }

                    return await base.UpdateAsync(dto);
                }

                return new ServiceResponse<PlaceDto>
                {
                    Success = false,
                    Message = "User not found.",
                    StatusCode = 404
                };
            }

            return new ServiceResponse<PlaceDto>
            {
                Success = false,
                Message = "Place not found.",
                StatusCode = 404
            };
        }

        public override async Task<ServiceResponse<PlaceDto>> DeleteAsync(string id)
        {
            string userId = _userContext.UserId;

            try
            {
                Place? place = await _repo.GetByIdAsync(id);

                if(place is null)
                {
                    return new ServiceResponse<PlaceDto>
                    {
                        Success = false,
                        Message = "Place not found",
                        StatusCode = 404
                    };
                }
                else
                {
                    var userResult = await _userService.GetByIdAsync(userId);

                    if (userResult.Success && userResult.Data is not null)
                    {
                        if (userResult.Data.Role != UserRole.Admin || place.UserId != userId)
                        {
                            return new ServiceResponse<PlaceDto>
                            {
                                Success = false,
                                StatusCode = 403
                            };
                        }
                    }  
                }
            }
            catch (Exception e)
            {
                return new ServiceResponse<PlaceDto>
                {
                    Success = false,
                    Message = $"An error occured while verifying user permissions: {e.Message}",
                    StatusCode = 500
                };
            }

            return await base.DeleteAsync(id);
        }

        public async Task<ServiceResponse<IEnumerable<PlaceDto>>> FilterPlacesAsync(PlaceFilterDto filter)
        {
            IQueryable<Place> query = _repo.GetAllAsQueryable();

            try
            {
                if (filter.Name is not null)
                {
                    FilterByName(filter.Name, ref query);
                }

                if (filter.CityId is not null)
                {
                    FilterByCity(filter.CityId, ref query);
                }

                if (filter.Category is not null)
                {
                    FilterByCategory(filter.Category, ref query);
                }

                if (filter.MaxDistanceKm is not null)
                {
                    double distanceKm = double.Parse(filter.MaxDistanceKm.ToString()!);
                    query = _placeRepo.GetNearbyQueryable(filter.Latitude, filter.Longitude, distanceKm);
                }

                List<Place> places = await query.ToListAsync();
                List<PlaceDto> result = places.Select(_assembler.ConvertToDto).ToList();

                return new ServiceResponse<IEnumerable<PlaceDto>>
                {
                    Data = result,
                    Success = true,
                    Message = "Places filtered successfully",
                    StatusCode = 200
                };
            }
            catch(Exception e)
            {
                return new ServiceResponse<IEnumerable<PlaceDto>>
                {
                    Data = null,
                    Success = false,
                    Message = $"An error occurred while filtering places: {e.Message}",
                    StatusCode = 500
                };
            }
        }

        private static void FilterByName(string name, ref IQueryable<Place> query)
        {
            query = query.Where(p => p.Name.Contains(name));
        }

        private static void FilterByCity(string cityId, ref IQueryable<Place> query)
        {
            query = query.Where(p => p.CityId == cityId);
        }

        private static void FilterByCategory(PlaceCategory? category, ref IQueryable<Place> query)
        {
            query = query.Where(p => p.Category! == category);
        }
    }
}
