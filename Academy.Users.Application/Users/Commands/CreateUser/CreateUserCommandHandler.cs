using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Academy.Users.Application.Users.Services;
using Academy.Users.Domain.Entities;
using Academy.Users.Domain.Exceptions;
using Academy.Users.Domain.Repositories;
using Academy.Users.Domain.Shared;
using MediatR;

namespace Academy.Users.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<CreateUserCommandResponse>>
{
    private static readonly EmailAddressAttribute EmailValidator = new();
    private static readonly Regex SpecialCharacterRegex = new("[!@#$%^&*(),.?\\\"{}|<>_+-=]", RegexOptions.Compiled);

    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<CreateUserCommandResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Request is null)
        {
            throw new NullCredentialException();
        }

        EnsureRequiredFields(request.Request);
        EnsureValidEmail(request.Request.Email);

        var emailExists = await _userRepository.EmailExistsAsync(request.Request.Email, cancellationToken);
        if (emailExists)
        {
            throw new DuplicateEmailException(request.Request.Email);
        }

        EnsureStrongPassword(request.Request.Password);

        var passwordHash = _passwordHasher.HashPassword(request.Request.Password);
        var creationDate = DateTime.UtcNow;
        var user = User.Create(
            request.Request.FirstName,
            request.Request.LastName,
            request.Request.Email,
            request.Request.Address,
            request.Request.PhoneNumber,
            passwordHash,
            creationDate,
            status: true);

        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        var response = new CreateUserCommandResponse
        {
            UserId = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            CreationDate = user.CreationDate,
            Status = user.Status
        };

        return Result<CreateUserCommandResponse>.Success(response);
    }

    private static void EnsureRequiredFields(CreateUserCommandRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FirstName) ||
            string.IsNullOrWhiteSpace(request.LastName) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Address) ||
            string.IsNullOrWhiteSpace(request.PhoneNumber) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            throw new NullCredentialException();
        }
    }

    private static void EnsureValidEmail(string email)
    {
        if (!EmailValidator.IsValid(email))
        {
            throw new InvalidEmailFormatException(email);
        }
    }

    private static void EnsureStrongPassword(string password)
    {
        if (password.Length < 8)
        {
            throw new WeakPasswordException();
        }

        if (!password.Any(char.IsUpper) ||
            !password.Any(char.IsLower) ||
            !password.Any(char.IsDigit) ||
            !SpecialCharacterRegex.IsMatch(password))
        {
            throw new WeakPasswordException();
        }
    }
}
