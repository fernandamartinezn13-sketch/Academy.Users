using Academy.Users.Domain.Entities.UserEntity.Models;
using Microsoft.EntityFrameworkCore;

namespace Academy.Users.Infrastructure.Persistence.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable("Users")
            .HasKey(u => u.UserId); 
 
    }
}
