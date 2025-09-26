using Academy.Users.Presentation.Modules;
using Academy.Users.Presentation.Modules.PasswordRecovery;

namespace Academy.Users.API;

public static class ModulesConfiguration
{
    public static IEndpointRouteBuilder Configure(this IEndpointRouteBuilder app)
    {
        app.MapGroup("/api")
            .MapPasswordRecoveryModule();

        return app;
    }
}