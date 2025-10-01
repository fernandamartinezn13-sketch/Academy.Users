namespace Academy.Users.Application.Users.Commands.UserManagement;

public class ManagementUserCommandResponse
{

    public string? email { get; set; } = default!;
    public int status { get; set; }
    public string message { get; set; } = default!;
    public DateTime timestamp { get; set; }
}
