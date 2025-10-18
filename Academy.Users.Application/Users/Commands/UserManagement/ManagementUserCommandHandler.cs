using Academy.Users.Domain.Entities.UserEntity.Repositories;
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
            // Validación del request
            if (request == null || string.IsNullOrWhiteSpace(request.request.Email))
                return Result<ManagementUserCommandResponse>.Failure(new Error("InvalidRequest", "El request no puede ser null o sin email."));

            // Obtener usuario
            var user = await _userRepository.GetByEmailAsync(request.request.Email);
            if (user == null)
                return Result<ManagementUserCommandResponse>.Failure(new Error("UserNotFound", "El usuario no existe."));

            // Actualizar estado
            user.Status = request.request.Status == 1 ? "ACTIVE" : "BLOCKED";
            user.UpdatedAt = DateTime.UtcNow;

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

            return Result<ManagementUserCommandResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<ManagementUserCommandResponse>.Failure(new Error("InternalError", ex.Message));
        }
    }
}
