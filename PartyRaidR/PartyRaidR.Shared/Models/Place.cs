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
            GpsLattitude = string.Empty;
            GpsLongitude = string.Empty;
            Description = string.Empty;
        }

        public Place(Guid id, string name, string address, string cityId, PlaceCategory category, string gpsLattitude, string gpsLongitude, string description)
        {
            Id = id.ToString();
            Name = name;
            Address = address;
            CityId = cityId;
            Category = category;
            GpsLattitude = gpsLattitude;
            GpsLongitude = gpsLongitude;
            Description = description;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CityId { get; set; }
        public PlaceCategory Category { get; set; }
        public string GpsLattitude { get; set; }
        public string GpsLongitude { get; set; }
        public string Description { get; set; }

        public override string ToString() =>
            $"{Name} | {Category}";
    }
}
