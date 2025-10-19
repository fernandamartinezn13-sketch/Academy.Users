using Academy.Users.Domain.Entities.UserEntity.Models;

namespace Academy.Users.Domain.Entities.UserEntity;

public class UserEntity
{
    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Status { get; set; } = "ACTIVE";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static implicit operator UserEntity?(User? v)
    {
        throw new NotImplementedException();
    }
}
