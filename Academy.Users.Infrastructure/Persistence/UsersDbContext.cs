using Microsoft.EntityFrameworkCore;
using Academy.Users.Domain.Users.Entities;

namespace Academy.Users.Infrastructure.Persistence;

public class UsersDbContext : DbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(b =>
        {
            b.ToTable("Users_login");
            b.HasKey(x => x.Id);
            b.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
            b.Property(x => x.LastName).HasMaxLength(100).IsRequired();
            b.Property(x => x.Email).HasMaxLength(256).IsRequired();
            b.HasIndex(x => x.Email).IsUnique();
            b.Property(x => x.PasswordHash).HasMaxLength(500).IsRequired();
            b.Property(x => x.IsActive).HasDefaultValue(true);
            b.Property(x => x.IsBlocked).HasDefaultValue(false);
        });
    }
}
