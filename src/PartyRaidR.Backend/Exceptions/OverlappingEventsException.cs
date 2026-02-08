namespace PartyRaidR.Backend.Exceptions
{
    [Serializable]
    internal class OverlappingEventsException : Exception
    {
        public OverlappingEventsException()
        {
        }

        public OverlappingEventsException(string? message) : base(message)
        {
        }

        public OverlappingEventsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}