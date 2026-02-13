using PartyRaidR.Backend.Converters;
using PartyRaidR.Backend.Models;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Assemblers
{
    public class NotificationAssembler : Assembler<Notification, NotificationDto>
    {
        public override NotificationDto ConvertToDto(Notification model) =>
            model.ConvertToNotificationDto();

        public override Notification ConvertToModel(NotificationDto dto) =>
            dto.ConvertToNotification();
    }
}
