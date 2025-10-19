using Academy.Users.Application.Shared;
using Academy.Users.Domain.Entities.UserEntity.Models;
using Academy.Users.Domain.Entities.UserEntity.Repositories;
using Academy.Users.Domain.Exceptions;
using MediatR;

namespace Academy.Users.Application.Users.Commands.UserManagement;
public class ManagementUserCommandHandler : IRequestHandler<ManagementUserCommand, Result<ManagementUserCommandResponse>>
{
    private readonly IUserRepository _userRepository;

    public ManagementUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<ManagementUserCommandResponse>> Handle(
        ManagementUserCommand request,
        CancellationToken cancellationToken)
    {
        
        if (request?.request == null)
            throw new InvalidRequestDataException("The request cannot be null.");

        if (string.IsNullOrWhiteSpace(request.request.Email) || string.IsNullOrEmpty(request.request.Email))
            throw new InvalidRequestDataException("Email is required.");
     

        try
        {
            
            UserModel? user = await _userRepository.GetByEmailAsync(request.request.Email);
            if (user == null)
                throw new InvalidRequestDataException("The user does not exist.");
   
            if (user.Status == "ACTIVE" && request.request.Status.Value)
                throw new UserAlreadyInStateException("The user is already active.");

            if (user.Status == "BLOCKED" && !request.request.Status.Value)
                throw new UserAlreadyInStateException("The user is already blocked.");

            
            user.Status = request.request.Status.Value ? "ACTIVE" : "BLOCKED";
            user.UpdatedAt = DateTime.Now;

            await _userRepository.UpdateAsync(user);

            
            ManagementUserCommandResponse response = new ManagementUserCommandResponse
            {
                Email = user.Email,
                Status = request.request.Status.Value,
                Message = request.request.Status.Value ? "Activated user" : "Blocked user",
                Timestamp = DateTime.Now
            };

            return Result<ManagementUserCommandResponse>.Success(response, response.Message);
        }
        catch (InvalidRequestDataException)
        {
            throw;
        }
        catch (InvalidStatusException)
        {
            throw;
        }
        catch (UserAlreadyInStateException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception("Internal error in ManagementUserCommandHandler: " + ex.Message);
        }
    }

}
