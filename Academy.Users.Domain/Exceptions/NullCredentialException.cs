namespace Academy.Users.Domain.Exceptions;

public class NullCredentialException : Exception
{
    public NullCredentialException()
        : base("Encrypted credentials were not provided.")
    {
    }
}
