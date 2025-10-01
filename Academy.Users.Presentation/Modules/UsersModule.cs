using Academy.Users.Application.Users.Commands.UserLogin;
using MediatR;
using FluentValidation;

using Microsoft.AspNetCore.Builder;   // MapPost
using Microsoft.AspNetCore.Http;      // Results, StatusCodes
using Microsoft.AspNetCore.Routing;   // IEndpointRouteBuilder

namespace Academy.Users.Presentation.Modules;

public static class UsersModule
{
    public static IEndpointRouteBuilder MapUsersModule(this IEndpointRouteBuilder app)
    {
        // NO crees new UserLoginCommand(); deja que el model binding lo cargue desde el body.
        app.MapPost("/login", async (
            UserLoginCommand cmd,                               // <- llega del JSON: { email, password }
            IValidator<UserLoginCommand> validator,
            ISender sender) =>
        {
            // Validación
            var v = await validator.ValidateAsync(cmd);
            if (!v.IsValid)
                return Results.BadRequest(new { message = "Missing or invalid fields", errors = v.Errors });

            // Enviar al handler
            var res = await sender.Send(cmd);

            // ¡NO usar .Value! 'res' YA es el objeto de respuesta.
            return res.HttpStatus switch
            {
                200 => Results.Ok(res),
                403 => Results.StatusCode(StatusCodes.Status403Forbidden),
                _ => Results.BadRequest(new { message = "Invalid credentials" })
            };
        })
        .WithTags("Auth")
        .Produces(200)
        .Produces(400)
        .Produces(403)
        .Produces(500);

        return app;
    }
}
