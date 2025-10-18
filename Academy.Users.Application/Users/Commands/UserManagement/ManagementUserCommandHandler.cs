using Academy.Users.Application.Shared;
using Academy.Users.Domain.Entities.UserEntity.Repositories;
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
        if (!((request?.request) != null && !string.IsNullOrWhiteSpace(request.request.Email) && !string.IsNullOrEmpty(request.request.Email)))
            return Result<ManagementUserCommandResponse>.Failure(
                new Error("InvalidRequest", "El request no puede ser null o sin email."));

        try
        {
            // Buscar usuario
            var user = await _userRepository.GetByEmailAsync(request.request.Email);
            if (user == null)
                return Result<ManagementUserCommandResponse>.Failure(
                    new Error("UserNotFound", "El usuario no existe."));

            // Actualizar estado
            user.Status = request.request.Status == 1 ? "ACTIVE" : "BLOCKED";
            user.UpdatedAt = DateTime.Now;

            // Guardar cambios
            await _userRepository.UpdateAsync(user);

            // Construir respuesta
            var response = new ManagementUserCommandResponse
            {
                Email = user.Email,
                Status = request.request.Status,
                Message = request.request.Status == 1 ? "Usuario activado" : "Usuario bloqueado",
                Timestamp = DateTime.Now
            };

            // Éxito con mensaje
            return Result<ManagementUserCommandResponse>.Success(
                response,
                response.Message
            );
        }
        catch (Exception ex)
        {
            return Result<ManagementUserCommandResponse>.Failure(
                new Error("InternalError", ex.Message)
            );
        }
    }
}
