using Academy.Users.Domain.Entities.UserEntity.Models;
using Academy.Users.Domain.Entities.UserEntity.Repositories;
using Academy.Users.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Academy.Users.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
