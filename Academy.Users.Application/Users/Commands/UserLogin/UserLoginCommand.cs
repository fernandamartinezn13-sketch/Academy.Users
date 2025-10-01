using MediatR;

namespace Academy.Users.Application.Users.Commands.UserLogin;


public sealed record UserLoginCommand(string Email, string Password)
    : IRequest<UserLoginCommandResponse>;
