namespace Academy.Users.Application.Users.Commands.UserRegister;

public class RegisterUserCommandResponse
{
    public int UserId { get; set; }
    public string? firstName { get; set; }
    public string? lastName { get; set; }
    public string? email { get; set; }
    public DateTime creationDate { get; set; }
    public bool status { get; set; }
}