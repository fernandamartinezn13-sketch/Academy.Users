using Academy.Users.Infrastructure.InjectionManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Academy.Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection addInfrastructureService(this IServiceCollection services)
    {
        RepositoriesManager.addRepositories(services);

        return services; 
    }
}
