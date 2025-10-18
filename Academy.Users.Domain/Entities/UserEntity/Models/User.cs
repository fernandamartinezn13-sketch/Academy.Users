using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.Users.Domain.Entities.UserEntity.Models;

public class User
{
    public int UserId { get; set; }
  
    public string Email { get; set; } = string.Empty;
    
    public string Status { get; set; } = "ACTIVE";
    
    public DateTime UpdatedAt { get; set; }

    public bool IsActive => Status == "ACTIVE";
}

