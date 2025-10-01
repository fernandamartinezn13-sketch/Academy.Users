using Academy.Users.Domain.Abstractions;                 // IJwtTokenGenerator, IPasswordHasher, IUserRepository
using Academy.Users.Domain.Exceptions;            // InvalidCredentialsException
using MediatR;

namespace Academy.Users.Application.Users.Commands.UserLogin;

/// <summary>
/// Lógica de autenticación: valida credenciales, estado del usuario y emite JWT.
/// </summary>
public sealed class UserLoginCommandHandler
    : IRequestHandler<UserLoginCommand, UserLoginCommandResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UserLoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<UserLoginCommandResponse> Handle(
        UserLoginCommand request,
        CancellationToken cancellationToken)
    {
        // 1) Buscar usuario por email (normalizado)
        var email = request.Email.Trim().ToLowerInvariant();
        var user = await _userRepository.GetByEmailAsync(email, cancellationToken);

        // 2) Credenciales inválidas
        if (user is null || !_passwordHasher.Verify(user.PasswordHash, request.Password))
            throw new InvalidCredentialsException();

        // 3) Usuario inactivo o bloqueado
        if (!user.IsActive || user.IsBlocked)
        {
            return new UserLoginCommandResponse(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                Token: string.Empty,
                Message: "User is inactive or blocked.",
                HttpStatus: 403
            );
        }

        // 4) Generar token
        var token = _jwtTokenGenerator.Generate(user);

        // 5) Respuesta exitosa
        return new UserLoginCommandResponse(
                user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            Token: token,
            Message: "Login successful.",
            HttpStatus: 200
        );
    }
}
