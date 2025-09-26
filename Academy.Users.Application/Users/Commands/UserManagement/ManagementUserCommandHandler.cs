using Academy.Users.Domain.Shared;
using MediatR;

namespace Academy.Users.Application.Users.Commands.UserManagement;

public class ManagementUserCommandHandler : IRequestHandler<ManagementUserCommand, Result<ManagementUserCommandResponse>>
{
public Task<Result<ManagementUserCommandResponse>> Handle(ManagementUserCommand request, CancellationToken cancellationToken)
{
    throw new NotImplementedException();
}
}
