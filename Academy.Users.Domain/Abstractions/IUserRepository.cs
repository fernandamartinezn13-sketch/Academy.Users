namespace Academy.Users.Domain.Abstractions;
using Academy.Users.Domain.Users.Entities;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken ct);
}