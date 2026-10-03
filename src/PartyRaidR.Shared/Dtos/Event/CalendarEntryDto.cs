using PartyRaidR.Shared.Enums;

namespace PartyRaidR.Shared.Dtos.Event;

public class CalendarEntryDto : IHasId
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; } = DateOnly.MinValue;
    public DateOnly EndDate { get; set; } = DateOnly.MinValue;
    public bool IsAuthor { get; set; } = false;
    public string LocationName { get; set; }
    public StatusType Status { get; set; } = StatusType.Pending;
}