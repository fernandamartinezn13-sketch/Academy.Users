using Academy.Users.Domain.Entities.UserEntity.Repositories;
using Academy.Users.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Users.Infrastructure.InjectionManager;

public static class RepositoriesManager
{
    public static IServiceCollection addRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
