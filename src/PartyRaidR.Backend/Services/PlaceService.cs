using Microsoft.EntityFrameworkCore;
using PartyRaidR.Backend.Assemblers;
using PartyRaidR.Backend.Exceptions;
using PartyRaidR.Backend.Models;
using PartyRaidR.Backend.Models.Responses;
using PartyRaidR.Backend.Repos.Promises;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Enums;

namespace PartyRaidR.Backend.Services
{
    public class PlaceService : BaseService<Place, PlaceDto>, IPlaceService
    {
        private readonly IPlaceRepo _placeRepo;
        private readonly IUserRepo _userRepo;
        private readonly ICityRepo _cityRepo;
        private readonly IUserService _userService;

        public PlaceService(PlaceAssembler? assembler, IPlaceRepo? repo, IUserRepo? userRepo, ICityRepo? cityRepo, IUserContext userContext, IUserService userService) : base(assembler, repo, userContext)
        {
            _placeRepo = repo!;
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            _cityRepo = cityRepo ?? throw new ArgumentNullException(nameof(cityRepo));
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

            try
            {
                Place place = await _repo.GetByIdAsync(dto.Id) ?? throw new EntityNotFoundException("Place not found.");

                bool isUserAuthor = await IsUserAuthor(place);
                if (!isUserAuthor)
                    return CreateResponse<PlaceDto>(false, 403, message: "You do not have permission to update this place.");

                bool cityExists = await _cityRepo.ExistsAsync(c => c.Id == dto.CityId);
                if (!cityExists)
                    throw new EntityNotFoundException("The specified city does not exist.");

                place = _assembler.ConvertToModel(dto);
                place.UserId = userId;

                _repo.Update(place);
                await _repo.SaveChangesAsync();

                return CreateResponse<PlaceDto>(true, 200, message: "Place updated successfully.");
            }
            catch(EntityNotFoundException ex)
            {
                return CreateResponse<PlaceDto>(false, 404, message: ex.Message);
            }
            catch (Exception ex)
            {
                return CreateResponse<PlaceDto>(false, 500, message: $"An error occured while verifying user permissions: {ex.Message}");
            }
        }

        public override async Task<ServiceResponse<PlaceDto>> DeleteAsync(string id)
        {
            string userId = _userContext.UserId;

            try
            {
                Place? place = await _repo.GetByIdAsync(id);

                if (place is null)
                    return CreateResponse<PlaceDto>(false, 404, message: "Place not found.");
                else
                {
                    bool isUserAuthor = await IsUserAuthor(place);

                    if (!isUserAuthor)
                        return CreateResponse<PlaceDto>(false, 403, message: "You do not have permission to delete this place.");

                    return await base.DeleteAsync(id);
                }
            }
            catch (Exception e)
            {
                return CreateResponse<PlaceDto>(false, 500, message: $"An error occured while verifying user permissions: {e.Message}");
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

                return CreateResponse<IEnumerable<PlaceDto>>(true, 200, result, "Places filtered successfully");
            }
            catch(Exception e)
            {
                return CreateResponse<IEnumerable<PlaceDto>>(false, 500, message: $"An error occurred while filtering places: {e.Message}");
            }
        }

        public async Task<ServiceResponse<IEnumerable<PlaceDto>>> GetMyPlacesAsync()
        {
            try
            {
                var places = await _placeRepo.FindByConditionAsync(p => p.UserId == _userContext.UserId);

                var result = places.Select(_assembler.ConvertToDto).ToList();

                return CreateResponse<IEnumerable<PlaceDto>>(true,
                                                             200,
                                                             result,
                                                             (places is null || places.Count() == 0)
                                                                              ? "This user have no places yet."
                                                                              : string.Empty);
            }
            catch(Exception e)
            {
                return CreateResponse<IEnumerable<PlaceDto>>(false, 500, message: $"An error occurred while retrieving your places: {e.Message}");
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
