namespace Academy.Users.Application.Users.Commands.UserManagement;

public class ManagementUserCommandResponse
{
    public string Email { get; set; } = default!;
    public bool Status { get; set; }
    public string Message { get; set; } = default!;
    public DateTime Timestamp { get; set; }

}
