namespace Academy.Users.Domain.Exceptions;

public sealed class InvalidRequestDataException : Exception
{
    public InvalidRequestDataException(string message) : base(message) { }
}
