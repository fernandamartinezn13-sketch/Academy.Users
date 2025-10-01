namespace Academy.Users.Domain.Entities.UserEntity;

public class UserHistory
{
    public int history_id { get; set; }       // history_id
    public int user_id { get; set; }          // FK a User
    public string event_type { get; set; } = default!;   // BLOCKED, ACTIVATED, LOGIN_FAILED
    public string? details { get; set; }                // Reason o info adicional
    public DateTime created_at { get; set; } = DateTime.UtcNow;
}


