namespace Academy.Users.Application.Users.Commands.UpdateUserProfile;

public class UpdateUserProfileCommandResponse
{
    public int userId { get; set; }
    public string? firstName { get; set; }
    public string? lastName { get; set; }
    public string? phoneNumber { get; set; }
    public string? adress { get; set; }
    public bool status { get; set; }
    public int message { get; set; }
}
