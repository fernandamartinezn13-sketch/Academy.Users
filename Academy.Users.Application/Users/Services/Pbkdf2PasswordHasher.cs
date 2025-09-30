using System.Security.Cryptography;
using System.Text;

namespace Academy.Users.Application.Users.Services;

public sealed class Pbkdf2PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 100_000;

    public string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
        var key = pbkdf2.GetBytes(KeySize);
        var saltedHash = new byte[SaltSize + KeySize];
        Buffer.BlockCopy(salt, 0, saltedHash, 0, SaltSize);
        Buffer.BlockCopy(key, 0, saltedHash, SaltSize, KeySize);
        return Convert.ToBase64String(saltedHash);
    }
}
