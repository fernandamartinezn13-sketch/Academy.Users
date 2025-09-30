using Academy.Users.Domain.Repositories;
using Academy.Users.Domain.Services;
using Academy.Users.Infrastructure.Configuration;
using Academy.Users.Infrastructure.Context;
using Academy.Users.Infrastructure.Repositories;
using Academy.Users.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Academy.Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.Configure<EncryptionSettings>(configuration.GetSection("Encryption"));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<IEncryptionService, AesEncryptionService>();

        return services;
    }
}
