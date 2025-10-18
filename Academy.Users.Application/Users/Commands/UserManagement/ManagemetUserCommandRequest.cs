namespace Academy.Users.Application.Users.Commands.UserManagement;

public class ManagemetUserCommandRequest
{
    public string Email { get; set; } = default!;
    public int Status { get; set; } // 1 = Active, 0 = Blocked

}
