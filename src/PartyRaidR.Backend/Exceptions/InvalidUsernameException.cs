[Serializable]
internal class InvalidUsernameException : Exception
{
    public InvalidUsernameException()
    {
    }
    
    public InvalidUsernameException(string? message) : base(message)
    {
    }

    public InvalidUsernameException(string? message, Exception? innerException): base(message, innerException)
    {
    }
}