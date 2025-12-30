namespace PartyRaidR.Backend.Exceptions
{
    [Serializable]
    internal class UserDoesNotMeetRequiredAgeException : Exception
    {
        public UserDoesNotMeetRequiredAgeException()
        {
        }

        public UserDoesNotMeetRequiredAgeException(string? message) : base(message)
        {
        }

        public UserDoesNotMeetRequiredAgeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}