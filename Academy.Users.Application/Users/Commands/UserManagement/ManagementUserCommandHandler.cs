using Academy.Users.Domain.Entities.UserEntity.Repositories;
using Academy.Users.Domain.Shared;
using MediatR;

namespace Academy.Users.Application.Users.Commands.UserManagement;

public class ManagementUserCommandHandler(IUserRepository userRepository) : IRequestHandler<ManagementUserCommand, Result<ManagementUserCommandResponse>>
{

    private readonly IUserRepository _userRepository = userRepository;
public Task<Result<ManagementUserCommandResponse>> Handle(ManagementUserCommand request, CancellationToken cancellationToken)
{
    throw new NotImplementedException();
}
}
