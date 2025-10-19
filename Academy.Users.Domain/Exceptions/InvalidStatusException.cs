namespace Academy.Users.Domain.Exceptions;

public sealed class InvalidStatusException : Exception
{
    public InvalidStatusException(string message) : base(message) { }
}
