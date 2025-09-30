namespace Academy.Users.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommandResponse
{
    public Guid UserId { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public DateTime CreationDate { get; init; }
    public bool Status { get; init; }
}
