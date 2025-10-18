using Academy.Users.Domain.Entities.UserEntity.Models;

namespace Academy.Users.Domain.Entities.UserEntity.Repositories;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email);
    Task UpdateAsync(User user);

}
