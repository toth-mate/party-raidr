using PartyRaidR.Backend.Exceptions;
using PartyRaidR.Backend.Models;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Enums;

namespace PartyRaidR.Backend.Services
{
    public partial class EventService
    {
        private async Task ValidateEvent(EventDto dto)
        {
            if (dto.Title.Trim() == string.Empty)
                throw new ArgumentException("Title cannot be empty.");

            if (dto.Description.Trim() == string.Empty)
                throw new ArgumentException("Description cannot be empty.");

            if (dto.StartingDate >= dto.EndingDate)
                throw new ArgumentException("Starting date must be before ending date.");

            if (dto.StartingDate <= DateTime.UtcNow)
                throw new ArgumentException("Starting date must be in the future.");

            if(dto.StartingDate < DateTime.UtcNow.AddHours(3))
                throw new ArgumentException("Starting date must be at least 3 hours from now.");

            if(dto.EndingDate < dto.StartingDate.AddMinutes(20))
                throw new ArgumentException("Event duration must be at least 20 minutes.");

            if (dto.TicketPrice < 0)
                throw new ArgumentException("Ticket price cannot be negative.");

            Place? place = await _placeRepo.GetByIdAsync(dto.PlaceId);

            if (dto.PlaceId is null || dto.PlaceId == string.Empty || place is null)
                throw new ArgumentException("Invalid place.");

            if (dto.Room < 0 || dto.Room == 1)
                throw new ArgumentException("Room must be greater than 1.");

            List<Event> eventsAtPlace = await _eventRepo.FilterEventsAsync(null, null, null, null, null, dto.PlaceId, null, null, null, null);

            // Check for overlapping events at the same place
            bool isOverlapping = eventsAtPlace.Any(e =>
                e.Id != dto.Id
                && (dto.StartingDate <= e.StartingDate && dto.EndingDate > e.StartingDate)
                || (dto.StartingDate > e.StartingDate && dto.StartingDate < e.EndingDate)
            );

            if(isOverlapping)
                throw new OverlappingEventsException("An event is already scheduled at this place during the specified time.");

            dto.DateCreated = DateTime.UtcNow;
            dto.AuthorId = _userContext.UserId;
            dto.IsActive = true;
        }

        private async Task<bool> IsUserAuthor(Event eventToEdit)
        {
            string userId = _userContext.UserId;
            var userResult = await _userService.GetByIdAsync(userId);

            return (userResult.Success && userResult.Data is not null) && (userResult.Data.Role != UserRole.Admin && eventToEdit.AuthorId == userId);
        }
    }
}
