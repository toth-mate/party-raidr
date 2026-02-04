using PartyRaidR.Backend.Models;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Enums;

namespace PartyRaidR.Backend.Services
{
    public partial class EventService
    {
        private async Task ValidateNewEvent(EventDto dto)
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

            if (dto.Room < 0 || dto.Room == 1)
                throw new ArgumentException("Room must be greater than 1.");

            dto.DateCreated = DateTime.UtcNow;
            dto.AuthorId = _userContext.UserId;
        }

        private async Task<bool> IsUserAuthor(Event eventToEdit)
        {
            string userId = _userContext.UserId;
            var userResult = await _userService.GetByIdAsync(userId);

            return (userResult.Success && userResult.Data is not null) && (userResult.Data.Role != UserRole.Admin && eventToEdit.AuthorId == userId);
        }
    }
}
