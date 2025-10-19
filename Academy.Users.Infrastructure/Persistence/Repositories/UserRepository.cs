using Academy.Users.Domain.Entities.UserEntity;
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
    
    public async Task<UserModel?> GetByEmailAsync(string email)
    {
        UserEntity entity = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (entity == null)
            return null;
        
        UserModel user = new UserModel
        {
            UserId = entity.UserId,
            Email = entity.Email,
            Status = entity.Status,
            UpdatedAt = entity.UpdatedAt
        };
        return user;
    }

    public async Task UpdateAsync(UserModel user)
    {
        UserEntity entity = await _context.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);

        if (entity == null)
            throw new Exception("User not found in DB");

        entity.Status = user.Status;
        entity.UpdatedAt = DateTime.Now;

        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
    }
}

