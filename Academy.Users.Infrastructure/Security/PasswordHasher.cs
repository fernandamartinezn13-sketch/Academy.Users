using Microsoft.AspNetCore.Identity;
using Academy.Users.Domain.Abstractions;

namespace Academy.Users.Infrastructure.Security;

public class PasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<object> _impl = new();
    public string Hash(string password) => _impl.HashPassword(new(), password);
    public bool Verify(string hash, string password) =>
        _impl.VerifyHashedPassword(new(), hash, password) is
            PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded;
}
