using Microsoft.AspNetCore.Builder;

namespace Academy.Users.Presentation.Modules;

public class ModulesConfiguration
{
    public static void Configure(WebApplication app)
    {
        app.AddUsersModules();
    }
}