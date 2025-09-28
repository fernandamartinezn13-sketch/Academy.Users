using Academy.Users.Domain.Entities.UserEntity;
using Microsoft.EntityFrameworkCore;

namespace Academy.Users.Infrastructure.Persistence.Context;

public partial class ApplicationDbContextObjects
{
    public virtual DbSet<User> Users { get; set; }
}
