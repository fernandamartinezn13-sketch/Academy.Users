using Academy.Users.Application.Users.Commands.UserLogin;
using MediatR;

using Microsoft.AspNetCore.Builder;   // MapPost
using Microsoft.AspNetCore.Http;      // Results, StatusCodes
using Microsoft.AspNetCore.Routing;   // IEndpointRouteBuilder

namespace Academy.Users.Presentation;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (UserLoginCommand cmd, ISender sender) =>
        {
            var res = await sender.Send(cmd);

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

        //OTRA PRUEBA
        // --- GET /me  (PROTEGIDO: requiere JWT) ---
        app.MapGet("/me", (HttpContext ctx) =>
        {
            // Revisa los claims del token
            var sub = ctx.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            var email = ctx.User.Claims.FirstOrDefault(c => c.Type.Contains("email"))?.Value;

            return Results.Ok(new { sub, email });
        })
        .RequireAuthorization()      // <- pide un Bearer token válido
        .WithTags("Auth")
        .Produces(200)
        .Produces(401);

        return app;
    }
}
