namespace PartyRaidR.Shared.Models.Responses
{
    public class ServiceResponse<T>
    {
        public object? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
    }
}
