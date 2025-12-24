using PartyRaidR.Shared.Converters;
using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Shared.Assemblers
{
    public class NotificationAssembler : Assembler<Notification, NotificationDto>
    {
        public override NotificationDto ConvertToDto(Notification model) =>
            model.ConvertToNotificationDto();

        public override Notification ConvertToModel(NotificationDto dto) =>
            dto.ConvertToNotification();
    }
}
