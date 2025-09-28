namespace Academy.Users.Domain.Entities.UserEntity.Repositories;

public interface IUserRepository
{
    Task<User> GetUserById(int userId);
}
