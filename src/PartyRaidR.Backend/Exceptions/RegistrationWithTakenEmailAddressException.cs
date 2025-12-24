namespace PartyRaidR.Backend.Exceptions
{
    [Serializable]
    internal class RegistrationWithTakenEmailAddressException : Exception
    {
        public RegistrationWithTakenEmailAddressException()
        {
        }

        public RegistrationWithTakenEmailAddressException(string? message) : base(message)
        {
        }

        public RegistrationWithTakenEmailAddressException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}