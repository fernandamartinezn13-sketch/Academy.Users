using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using Academy.Users.Application.Users.Commands.CreateUser;
using Academy.Users.Domain.Exceptions;
using Academy.Users.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Academy.Users.Presentation.Modules;

public static class UsersModule
{
    private const string RoutePrefix = "/users";
    private static readonly JsonSerializerOptions PayloadSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    public static void AddUsersModules(this IEndpointRouteBuilder app)
    {
        var usersGroup = app.MapGroup(RoutePrefix);
        usersGroup.MapPost(string.Empty, CreateUser);
    }

    private static async Task<IResult> CreateUser(
        [FromBody] EncryptedUserRegistrationRequest request,
        IEncryptionService encryptionService,
        ISender sender,
        CancellationToken cancellationToken)
    {
        try
        {
            if (request is null || string.IsNullOrWhiteSpace(request.Content))
            {
                throw new NullCredentialException();
            }

            var decryptedPayload = encryptionService.Decrypt(request.Content);
            var payload = JsonSerializer.Deserialize<UserRegistrationPayload>(decryptedPayload, PayloadSerializerOptions);

            if (payload is null)
            {
                throw new NullCredentialException();
            }

            var commandRequest = new CreateUserCommandRequest
            {
                FirstName = payload.FirstName,
                LastName = payload.LastName,
                Email = payload.Email,
                Address = payload.Address,
                PhoneNumber = payload.PhoneNumber,
                Password = payload.Password
            };

            var result = await sender.Send(new CreateUserCommand(commandRequest), cancellationToken);

            if (!result.IsSuccess || result.Value is null)
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Results.Created($"/api{RoutePrefix}/{result.Value.UserId}", result.Value);
        }
        catch (NullCredentialException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
        catch (InvalidEmailFormatException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
        catch (DuplicateEmailException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
        catch (WeakPasswordException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
        catch (FormatException)
        {
            return Results.BadRequest(new { error = "Invalid encrypted payload." });
        }
        catch (CryptographicException)
        {
            return Results.BadRequest(new { error = "Unable to decrypt payload." });
        }
        catch (JsonException)
        {
            return Results.BadRequest(new { error = "Invalid registration payload format." });
        }
        catch (Exception)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    private sealed class UserRegistrationPayload
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
