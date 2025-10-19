namespace Academy.Users.Domain.Exceptions;

public class UserAlreadyInStateException : Exception
{
    public UserAlreadyInStateException(string message) : base(message) { }
}

