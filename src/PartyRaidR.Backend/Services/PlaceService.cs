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

        public PlaceService(Assembler<Place, PlaceDto>? assembler, IPlaceRepo? repo) : base(assembler, repo)
        {
            _placeRepo = repo!;
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
