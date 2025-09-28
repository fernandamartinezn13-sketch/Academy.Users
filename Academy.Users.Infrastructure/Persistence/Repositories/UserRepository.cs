using Academy.Users.Domain.Entities.UserEntity;
using Academy.Users.Domain.Entities.UserEntity.Repositories;
using Academy.Users.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Academy.Users.Infrastructure.Persistence.Repositories;
                            
                            // va un contexto de bd INVESTIGAR
public class UserRepository(ApplicationDbContextObjects context) : IUserRepository
{
    public async Task<User> GetUserById(int userId)
    {
        // Aquí se hace la lógica para trabajar con los datos de la base de datos

        //IntEntityId user = new IntEntityId(userId);
        // return await DbContext.Users.FirstOfDefaultAsync(x => x.UserId == UserId);
        throw new NotImplementedException();
    }
}
