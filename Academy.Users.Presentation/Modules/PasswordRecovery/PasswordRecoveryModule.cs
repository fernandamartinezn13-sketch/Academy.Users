using Academy.Users.Infrastructure.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Academy.Users.Presentation.Modules.PasswordRecovery;

public static class PasswordRecoveryModule
{
    public static IEndpointRouteBuilder MapPasswordRecoveryModule(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/password-recovery");

        group.MapPost("/request", async (string email, ApplicationDbContext dbContext) =>
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return Results.NotFound("User not found");
            }

            // TODO: Generate and send recovery token
            return Results.Ok("Password recovery email sent");
        });

        return app;
    }
}