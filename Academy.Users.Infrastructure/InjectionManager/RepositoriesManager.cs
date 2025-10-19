using Academy.Users.Domain.Entities.UserEntity.Repositories;
using Academy.Users.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Academy.Users.Infrastructure.InjectionManager;

public static class RepositoriesManager
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
