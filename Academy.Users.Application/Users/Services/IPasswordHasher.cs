namespace Academy.Users.Application.Users.Services;

public interface IPasswordHasher
{
    string HashPassword(string password);
}
