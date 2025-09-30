using Academy.Users.Application.Users.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Academy.Users.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddScoped<IPasswordHasher, Pbkdf2PasswordHasher>();

            return services;
        }
    }
}
