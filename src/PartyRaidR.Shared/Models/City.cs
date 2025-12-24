namespace PartyRaidR.Shared.Models
{
    public class City : IDbEntity<City>
    {
        public City()
        {
            Id = Guid.Empty.ToString();
            Name = string.Empty;
            ZipCode = "0000";
            County = string.Empty;
            Country = string.Empty;
        }

        public City(Guid id, string name, string zipCode, string county, string country)
        {
            Id = id.ToString();
            Name = name;
            ZipCode = zipCode;
            County = county;
            Country = country;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public string County { get; set; }
        public string Country { get; set; }

        public override string ToString() =>
            $"{Name}, {County}";
    }
}
