namespace Academy.Users.Domain.Exceptions;

public class InvalidEmailFormatException : Exception
{
    public InvalidEmailFormatException(string email)
        : base($"The email '{email}' has an invalid format.")
    {
    }
}
