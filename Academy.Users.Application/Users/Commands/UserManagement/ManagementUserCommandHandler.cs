using Academy.Users.Domain.Entities.UserEntity;
using Academy.Users.Domain.Entities.UserEntity.Repositories;
using Academy.Users.Domain.Exceptions;
using Academy.Users.Domain.Shared;
using MediatR;

namespace Academy.Users.Application.Users.Commands.UserManagement;
public class ManagementUserCommandHandler : IRequestHandler<ManagementUserCommand, Result<ManagementUserCommandResponse>>
{

    private readonly IUserRepository _userRepository;

    public ManagementUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<ManagementUserCommandResponse>> Handle(ManagementUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByEmailAsync(request.request.email);

            if (user == null)
                return Result<ManagementUserCommandResponse>.Failure(new Error("UserNotFound", "El usuario no existe"));

            // Bloqueado temporal
            if (user.isBlocked)
                throw new UserBlockedException("Usuario bloqueado temporalmente");

            // Validación para activación
            if (request.request.status == 1) // Activar
            {
                if (!user.isEmailVerified || !user.passwordRecovered)
                {
                    return Result<ManagementUserCommandResponse>.Failure(
                        new Error("ValidationFailed", "Usuario no cumple las condiciones para activación")
                    );
                }
            }

            // Actualizar estado
            user.status = request.request.status == 1 ? "ACTIVE" : "BLOCKED";

            // Manejo de bloqueos por intentos fallidos
            if (request.request.status == 0)
            {
                user.blockedUntil = DateTime.UtcNow.AddMinutes(5);
            }
            else
            {
                user.blockedUntil = null;
                user.failedLoginAttempts = 0;
            }

            await _userRepository.UpdateAsync(user);

            // Crear response
            var response = new ManagementUserCommandResponse
            {
                email = user.email,
                status = request.request.status,
                message = request.request.status == 1 ? "Usuario activado" : "Usuario bloqueado",
                timestamp = DateTime.UtcNow
            };

            return Result<ManagementUserCommandResponse>.Success(response);
        }
        catch (UserBlockedException ex)
        {
            return Result<ManagementUserCommandResponse>.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result<ManagementUserCommandResponse>.Failure(new Error("InternalError", ex.Message));
        }
    }
}
