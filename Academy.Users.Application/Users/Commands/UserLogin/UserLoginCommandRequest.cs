namespace Academy.Users.Application.Users.Commands.UserLogin;

public class UserLoginCommandRequest
{
    public string firstName { get; set; } = string.Empty;
    public string lastName { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string address { get; set; } = string.Empty;
    public string phoneNumber { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;
}
