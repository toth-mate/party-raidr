namespace PartyRaidR.Shared.Dtos
{
    public class CityDto
    {
        public string Id { get; set; } = Guid.Empty.ToString();
        public string Name { get; set; } = string.Empty;
        public int ZipCode { get; set; } = 0;
        public string County { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
