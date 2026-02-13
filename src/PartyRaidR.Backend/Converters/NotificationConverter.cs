using PartyRaidR.Backend.Models;
using PartyRaidR.Shared.Dtos;

namespace PartyRaidR.Backend.Converters
{
    public static class NotificationConverter
    {
        public static NotificationDto ConvertToNotificationDto(this Notification notification) =>
            new NotificationDto()
            {
                Id = notification.Id,
                Title = notification.Title,
                Text = notification.Text,
                UserId = notification.UserId,
                EventId = notification.EventId,
                Type = notification.Type,
                DateCreated = notification.DateCreated,
                IsRead = notification.IsRead
            };

        public static Notification ConvertToNotification(this NotificationDto notificationDto) =>
            new Notification()
            {
                Id = notificationDto.Id,
                Title = notificationDto.Title,
                Text = notificationDto.Text,
                UserId = notificationDto.UserId,
                EventId = notificationDto.EventId,
                Type = notificationDto.Type,
                DateCreated = notificationDto.DateCreated,
                IsRead = notificationDto.IsRead
            };
    }
}
