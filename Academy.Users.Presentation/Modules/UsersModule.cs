using Academy.Users.Application.Users.Commands.UserRegister;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Academy.Users.Presentation.Modules;

public static class UsersModule
{
    private const string BASE_URL = "api/v1/user/";
    public static void AddUsersModules(this IEndpointRouteBuilder app)
    {
        var customerGroup = app.MapGroup(BASE_URL);

        customerGroup.MapPost("", CreateCustomer);
    }

    private static async Task<IResult> CreateCustomer(
        [FromBody] RegisterUserCommandRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(request);
        var result = await sender.Send(command, cancellationToken);

        if (result.Value == null)
            return Results.Content("Unable to create cart");

        return Results.Created($"{BASE_URL}{result.Value.UserId}", result.Value);
    }
}