using Academy.Users.Domain.Shared;
using MediatR;

namespace Academy.Users.Application.Users.Commands.UpdateUserProfile;
public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, Result<UpdateUserProfileCommandResponse>>
{
public Task<Result<UpdateUserProfileCommandResponse>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }    
}
