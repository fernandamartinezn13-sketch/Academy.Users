using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Academy.Users.Presentation.Modules.PasswordRecovery;

public static class PasswordRecoveryModule
{
    public static IEndpointRouteBuilder MapPasswordRecoveryModule(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/password-recovery");

        group.MapPost("/request", () => Results.StatusCode(StatusCodes.Status501NotImplemented));

        return app;
    }
}
