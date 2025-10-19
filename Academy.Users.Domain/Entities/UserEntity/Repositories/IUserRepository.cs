using Academy.Users.Domain.Entities.UserEntity.Models;

namespace Academy.Users.Domain.Entities.UserEntity.Repositories;

public interface IUserRepository
{
    Task<UserModel> GetByEmailAsync(string email);
    Task UpdateAsync(UserModel user);

}
