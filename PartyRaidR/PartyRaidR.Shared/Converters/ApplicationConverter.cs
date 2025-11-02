using PartyRaidR.Shared.Dtos;
using PartyRaidR.Shared.Models;

namespace PartyRaidR.Shared.Converters
{
    public static class ApplicationConverter
    {
        public static ApplicationDto ConvertToApplicationDto(this Application application) =>
            new ApplicationDto()
            {
                Id = application.Id,
                UserId = application.UserId,
                EventId = application.EventId,
                TimeOfApplication = application.TimeOfApplication,
                Status = application.Status
            };

        public static Application ConvertToApplication(this ApplicationDto applicationDto) =>
            new Application()
            {
                Id = applicationDto.Id,
                UserId = applicationDto.UserId,
                EventId = applicationDto.EventId,
                TimeOfApplication = applicationDto.TimeOfApplication,
                Status = applicationDto.Status
            };
    }
}
