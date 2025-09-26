using Academy.Users.Domain.Shared;
using MediatR;

namespace Academy.Users.Application.Users.Commands.UserLogin;

public sealed record UserLoginCommand(UserLoginCommandRequest request) : IRequest<Result<UserLoginCommandResponse>>;
