using Academy.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Academy.Users.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(255).IsRequired();
            entity.Property(e => e.Address).HasMaxLength(250).IsRequired();
            entity.Property(e => e.PhoneNumber).HasMaxLength(50).IsRequired();
            entity.Property(e => e.PasswordHash).HasMaxLength(512).IsRequired();
            entity.Property(e => e.CreationDate).IsRequired();
            entity.Property(e => e.Status).IsRequired();

            entity.HasIndex(e => e.Email).IsUnique();
        });
    }
}
