using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Academy.Users.Domain.Abstractions;
using Academy.Users.Domain.Users.Entities;

namespace Academy.Users.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UsersDbContext _db;
    public UserRepository(UsersDbContext db) => _db = db;

    public Task<User?> GetByEmailAsync(string email, CancellationToken ct) =>
        _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email, ct);
}
