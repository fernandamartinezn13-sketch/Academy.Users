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
            throw new InvalidRequestDataException("El request no puede ser null.");

        if (string.IsNullOrWhiteSpace(request.request.Email) || string.IsNullOrEmpty(request.request.Email))
            throw new InvalidRequestDataException("El email es obligatorio.");
     

        try
        {
            
            User? user = await _userRepository.GetByEmailAsync(request.request.Email);
            if (user == null)
                throw new InvalidRequestDataException("El usuario no existe.");
   
            if (user.Status == "ACTIVE" && request.request.Status.Value)
                throw new UserAlreadyInStateException("El usuario ya está ACTIVO.");

            if (user.Status == "BLOCKED" && !request.request.Status.Value)
                throw new UserAlreadyInStateException("El usuario ya está BLOQUEADO.");

            
            user.Status = request.request.Status.Value ? "ACTIVE" : "BLOCKED";
            user.UpdatedAt = DateTime.Now;

            await _userRepository.UpdateAsync(user);

            
            ManagementUserCommandResponse response = new ManagementUserCommandResponse
            {
                Email = user.Email,
                Status = request.request.Status.Value,
                Message = request.request.Status.Value ? "Usuario activado" : "Usuario bloqueado",
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
            throw new Exception("Error interno en ManagementUserCommandHandler: " + ex.Message);
        }
    }

}
