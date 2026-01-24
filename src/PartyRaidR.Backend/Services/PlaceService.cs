using Microsoft.EntityFrameworkCore;
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

        public override async Task<ServiceResponse<PlaceDto>> AddAsync(PlaceDto dto)
        {
            // Set the UserId to the current user's ID
            dto.UserId = _userContext.UserId;
            return await base.AddAsync(dto);
        }

        public override async Task<ServiceResponse<PlaceDto>> UpdateAsync(PlaceDto dto)
        {
            string userId = _userContext.UserId;

            var userResult = await _userService.GetByIdAsync(userId);

            Place? place = await _repo.GetByIdAsync(dto.Id);

            if (place is null)
            {
                return new ServiceResponse<PlaceDto>
                {
                    Success = false,
                    Message = "Place not found.",
                    StatusCode = 404
                };
            }
            else
            {
                bool isUserAuthor = await IsUserAuthor(place);

                if (!isUserAuthor)
                {
                    return new ServiceResponse<PlaceDto>
                    {
                        Success = false,
                        Message = "You do not have permission to update this place.",
                        StatusCode = 403
                    };
                }

                // Ensure the UserId is not changed
                PlaceDto updated = dto;
                updated.UserId = place.UserId;

                return await base.UpdateAsync(updated);
            }
        }

        public override async Task<ServiceResponse<PlaceDto>> DeleteAsync(string id)
        {
            string userId = _userContext.UserId;

            try
            {
                Place? place = await _repo.GetByIdAsync(id);

                if (place is null)
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
                    bool isUserAuthor = await IsUserAuthor(place);

                    if (!isUserAuthor)
                    {
                        return new ServiceResponse<PlaceDto>
                        {
                            Success = false,
                            Message = "You do not have permission to delete this place.",
                            StatusCode = 403
                        };
                    }

                    return await base.DeleteAsync(id);
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

        public async Task<ServiceResponse<IEnumerable<PlaceDto>>> GetMyPlacesAsync()
        {
            try
            {
                var places = await _placeRepo.FindByConditionAsync(p => p.UserId == _userContext.UserId);

                var result = places.Select(_assembler.ConvertToDto).ToList();

                return new ServiceResponse<IEnumerable<PlaceDto>>
                {
                    Success = true,
                    Data = result,
                    StatusCode = 200,
                    Message = (places is null || places.Count() == 0)
                                ? "This user have no places yet."
                                : string.Empty
                };
            }
            catch(Exception e)
            {
                return new ServiceResponse<IEnumerable<PlaceDto>>
                {
                    Data = null,
                    Success = false,
                    Message = $"An error occurred while retrieving your places: {e.Message}",
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

        private async Task<bool> IsUserAuthor(Place place)
        {
            string userId = _userContext.UserId;
            var userResult = await _userService.GetByIdAsync(userId);

            return (userResult.Success && userResult.Data is not null) && (userResult.Data.Role != UserRole.Admin && place.UserId == userId);
        }
    }
}
