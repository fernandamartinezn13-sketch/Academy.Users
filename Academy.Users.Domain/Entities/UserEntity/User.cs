using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.Users.Domain.Entities.UserEntity;


public class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
  
    public string Email { get; set; } = string.Empty;
    
    public string Status { get; set; } = "ACTIVE";
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }

    [NotMapped]
    public bool IsActive => Status == "ACTIVE";
}

