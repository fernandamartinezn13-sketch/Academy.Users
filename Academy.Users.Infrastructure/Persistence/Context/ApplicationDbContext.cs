using Academy.Users.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Academy.Users.Infrastructure.Persistence.Context;

public partial class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<DbContextOptionsUser> databaseOptions) : DbContext(options)
{
    private readonly IOptions<DbContextOptionsUser> _databaseOptions = databaseOptions;

    // aquí se agregan las configuraciones de las tablas 
}