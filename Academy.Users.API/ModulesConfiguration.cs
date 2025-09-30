using Academy.Users.Presentation.Modules;
using Academy.Users.Presentation.Modules.PasswordRecovery;

namespace Academy.Users.API;

public static class ModulesConfiguration
{
    public static IEndpointRouteBuilder Configure(this IEndpointRouteBuilder app)
    {
        var apiGroup = app.MapGroup("/api");

        apiGroup.AddUsersModules();
        apiGroup.MapPasswordRecoveryModule();

        return app;
    }
}
