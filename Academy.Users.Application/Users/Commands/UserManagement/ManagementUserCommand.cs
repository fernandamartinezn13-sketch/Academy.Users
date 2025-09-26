using Academy.Users.Domain.Shared;
using MediatR;


namespace Academy.Users.Application.Users.Commands.UserManagement;

public sealed record ManagementUserCommand(ManagemetUserCommandRequest request) : IRequest<Result<ManagementUserCommandResponse>>;

