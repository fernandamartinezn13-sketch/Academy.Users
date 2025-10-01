using Academy.Users.Domain.Abstractions;
using Academy.Users.Infrastructure.Auth;
using Academy.Users.Infrastructure.Persistence;
using Academy.Users.Infrastructure.Persistence.Repositories;
using Academy.Users.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Academy.Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration config)
    {
        // EF Core
        services.AddDbContext<UsersDbContext>(opt =>
            opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        // JWT
        var jwt = new JwtSettings();
        config.GetSection(JwtSettings.SectionName).Bind(jwt);
        services.AddSingleton(jwt);
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        // Seguridad y repos
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}

