using NetTopologySuite.Geometries;

namespace PartyRaidR.Shared.Models
{
    public enum PlaceCategory
    {
        None = 0,
        House = 1,
        Club = 2,
        PublicSpace = 3
    }

    public class Place : IDbEntity<Place>
    {
        public Place()
        {
            Id = Guid.Empty.ToString();
            Name = string.Empty;
            Address = string.Empty;
            CityId = string.Empty;
            Category = PlaceCategory.None;
            Location = new Point(0, 0);
            Description = string.Empty;
            UserId = string.Empty;
        }

        public Place(Guid id, string name, string address, string cityId, PlaceCategory category, Point location, string description, string userId)
        {
            Id = id.ToString();
            Name = name;
            Address = address;
            CityId = cityId;
            Category = category;
            Location = location;
            Description = description;
            UserId = userId;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CityId { get; set; }
        public PlaceCategory Category { get; set; }
        public Point Location { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }

        public override string ToString() =>
            $"{Name} | {Category}";
    }
}
