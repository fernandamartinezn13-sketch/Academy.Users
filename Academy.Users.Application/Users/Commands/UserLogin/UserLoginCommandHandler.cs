using Academy.Users.Domain.Shared;
using MediatR;

namespace Academy.Users.Application.Users.Commands.UserLogin;

public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, Result<UserLoginCommandResponse>>
{
    Task<Result<UserLoginCommandResponse>> IRequestHandler<UserLoginCommand, Result<UserLoginCommandResponse>>.Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
