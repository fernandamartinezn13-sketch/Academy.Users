using Academy.Users.Application.Users.Commands.UserLogin;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Academy.Users.Presentation.Modules;

public static class UsersModule
{
    private const string BASE_URL = "api/v1/userLogin/";
    public static void AddUsersModules(this IEndpointRouteBuilder app)
    {
        var customerGroup = app.MapGroup(BASE_URL);

        customerGroup.MapPost("", CreateCustomer);
    }

    private static async Task<IResult> CreateCustomer(
        [FromBody] UserLoginCommandRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new UserLoginCommand(request);
        var result = await sender.Send(command, cancellationToken);

        if (result.Value == null)
            return Results.Content("Unable to create cart");

        return Results.Created($"{BASE_URL}{result.Value.userId}", result.Value);
    }
}