using Academy.Users.Domain.Common.EntitiesBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.Users.Domain.Entities.UserEntity;

public class User
{
    public int user_id { get; set; }          // user_id en la BD
    public string first_name { get; set; } = default!;
    public string last_name { get; set; } = default!;
    public string email { get; set; } = default!;
    public string phone_number { get; set; } = default!;
    public string address { get; set; } = default!;
    public string password_hash { get; set; } = default!;
    public string status { get; set; } = "ACTIVE";  // ACTIVE, INACTIVE, BLOCKED
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }


    // Propiedades en memoria para la lógica
    [NotMapped]
    public int failedLoginAttempts { get; set; } = 0;

    [NotMapped]
    public DateTime? blockedUntil { get; set; }

    public bool isBlocked => status == "BLOCKED" || (blockedUntil.HasValue && blockedUntil.Value > DateTime.UtcNow);

    [NotMapped]
    public bool isEmailVerified { get; set; } = false;

    [NotMapped]
    public bool passwordRecovered { get; set; } = false;
}
