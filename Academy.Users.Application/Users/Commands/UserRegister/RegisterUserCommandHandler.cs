using Academy.Users.Domain.Shared;
using MediatR;

namespace Academy.Users.Application.Users.Commands.UserRegister;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<RegisterUserCommandResponse>>
{
    public Task<Result<RegisterUserCommandResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}