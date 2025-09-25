using Academy.Users.Domain.Shared;
using MediatR;

namespace Academy.Users.Application.Users.Commands.UserRegister;
public sealed record RegisterUserCommand(RegisterUserCommandRequest request) : IRequest<Result<RegisterUserCommandResponse>>;