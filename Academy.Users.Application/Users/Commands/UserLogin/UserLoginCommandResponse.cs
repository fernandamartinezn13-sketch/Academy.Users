namespace Academy.Users.Application.Users.Commands.UserLogin;

public class UserLoginCommandResponse
{
    public int userId { get; set; }
    public string? firstName { get; set; }
    public string? lastName { get; set; }
    public string? email { get; set; }
    public DateTime creationDate { get; set; }
    public bool status { get; set; }
}
