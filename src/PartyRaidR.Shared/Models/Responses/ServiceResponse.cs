namespace PartyRaidR.Shared.Models.Responses
{
    public class ServiceResponse<T>
    {
        public ServiceResponse()
        {
        }

        public T? Data { get; set; } = default(T);
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
    }
}
