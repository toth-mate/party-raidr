namespace PartyRaidR.Shared.Dtos.Event;

public class CalendarEntryDto : IHasId
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; } = DateOnly.MinValue;
    public bool IsAuthor { get; set; } = false;
}