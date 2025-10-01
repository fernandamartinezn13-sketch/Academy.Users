using Academy.Users.Domain.Shared;
using MediatR;

namespace Academy.Users.Application.Users.Commands.UpdateUserProfile;
public sealed record UpdateUserProfileCommand(UpdateUserProfileCommandRequest request) : IRequest<Result<UpdateUserProfileCommandResponse>>;
    