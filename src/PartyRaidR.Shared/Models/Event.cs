using System.ComponentModel.DataAnnotations.Schema;

namespace PartyRaidR.Shared.Models
{
    public enum EventCategory
    {
        None = 0,
        OutdoorsActivity = 1,
        IndoorsActivity = 2,
        Concert = 3,
        Festival = 4,
        Party = 5
    }

    public class Event : IDbEntity<Event>
    {
        public Event()
        {
            Id = Guid.Empty.ToString();
            Title = string.Empty;
            Description = string.Empty;
            StartingDate = DateTime.Now;
            EndingDate = DateTime.Now;
            PlaceId = string.Empty;
            Category = EventCategory.None;
            AuthorId = string.Empty;
            Room = 0;
            TicketPrice = 0;
            DateCreated = DateTime.Now;
        }

        public Event(Guid id, string title, string description, DateTime startingDate, DateTime endingDate, string placeId, EventCategory category, string authorId, int room, decimal ticketPrice, DateTime dateCreated)
        {
            Id = id.ToString();
            Title = title;
            Description = description;
            StartingDate = startingDate;
            EndingDate = endingDate;
            PlaceId = placeId;
            Category = category;
            AuthorId = authorId;
            Room = room;
            TicketPrice = ticketPrice;
            DateCreated = dateCreated;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string PlaceId { get; set; }
        public EventCategory Category { get; set; }
        public string AuthorId { get; set; }
        public int Room { get; set; }
        public decimal TicketPrice { get; set; }
        public DateTime DateCreated { get; set; }

        public Place Place { get; set; }

        [ForeignKey("AuthorId")]
        public User User { get; set; }
        public List<Application> Applications { get; set; } = new();

        public override string ToString() =>
            $"{Title}, {Room}";
    }
}
