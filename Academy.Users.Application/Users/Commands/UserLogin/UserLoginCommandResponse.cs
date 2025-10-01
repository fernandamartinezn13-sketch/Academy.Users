namespace Academy.Users.Application.Users.Commands.UserLogin;

public sealed record UserLoginCommandResponse(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    string Token,
    string Message,
    int HttpStatus
);