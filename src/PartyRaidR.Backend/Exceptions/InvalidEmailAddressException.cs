namespace PartyRaidR.Backend.Exceptions
{
    [Serializable]
    internal class InvalidEmailAddressException : Exception
    {
        public InvalidEmailAddressException()
        {
        }

        public InvalidEmailAddressException(string? message) : base(message)
        {
        }

        public InvalidEmailAddressException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}