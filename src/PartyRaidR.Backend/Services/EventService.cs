using PartyRaidR.Backend.Repos.Promises;
using PartyRaidR.Backend.Services.Base;
using PartyRaidR.Backend.Services.Promises;
using PartyRaidR.Shared.Assemblers;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;
using PartyRaidR.Shared.Models.Responses;

namespace PartyRaidR.Backend.Services
{
    public class EventService : BaseService<Event, EventDto>, IEventService
    {
        private readonly IPlaceRepo _placeRepo;

        public EventService(EventAssembler assembler, IEventRepo? repo, IUserContext userContext, IPlaceRepo? placeRepo) : base(assembler, repo, userContext)
        {
            _placeRepo = placeRepo ?? throw new ArgumentNullException(nameof(IPlaceRepo));
        }

        public override async Task<ServiceResponse<EventDto>> AddAsync(EventDto dto)
        {
            try
            {
                if (dto.Title.Trim() == string.Empty)
                    throw new ArgumentException("Title cannot be empty.");

                if (dto.Description.Trim() == string.Empty)
                    throw new ArgumentException("Description cannot be empty.");

                if (dto.StartingDate >= dto.EndingDate)
                    throw new ArgumentException("Starting date must be before ending date.");

                if (dto.StartingDate <= DateTime.UtcNow)
                    throw new ArgumentException("Starting date must be in the future.");

                if (dto.TicketPrice < 0)
                    throw new ArgumentException("Ticket price cannot be negative.");

                Place? place = await _placeRepo.GetByIdAsync(dto.PlaceId);

                if (dto.PlaceId is null || dto.PlaceId == string.Empty || place is null)
                    throw new ArgumentException("Invalid place.");

                if(dto.Room < 0 || dto.Room == 1)
                    throw new ArgumentException("Room must be greater than 1.");

                dto.DateCreated = DateTime.UtcNow;
                dto.AuthorId = _userContext.UserId;

                return await base.AddAsync(dto);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<EventDto>
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message,
                    StatusCode = 400
                };
            }
        }
    }
}
