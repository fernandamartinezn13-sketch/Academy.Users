using Academy.Users.Domain.Shared;
using MediatR;

namespace Academy.Users.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(CreateUserCommandRequest Request) : IRequest<Result<CreateUserCommandResponse>>;
