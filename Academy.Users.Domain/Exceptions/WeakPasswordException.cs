namespace Academy.Users.Domain.Exceptions;

public class WeakPasswordException : Exception
{
    public WeakPasswordException()
        : base("The provided password does not meet the security requirements.")
    {
    }
}
