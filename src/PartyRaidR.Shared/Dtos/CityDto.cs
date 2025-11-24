namespace PartyRaidR.Shared.Dtos
{
    public class CityDto
    {
        public string Id { get; set; } = Guid.Empty.ToString();
        public string Name { get; set; } = string.Empty;
        public string ZipCode { get; set; } = "0000";
        public string County { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
