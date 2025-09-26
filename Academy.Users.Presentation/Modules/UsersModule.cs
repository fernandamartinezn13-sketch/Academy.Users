using Academy.Users.Application.Users.Commands.UserManagement;
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
        customerGroup.MapPut("{email}", UpdateUser);
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

    private static async Task<IResult> UpdateUser(
        [FromBody] ManagemetUserCommandRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {

        var command = new ManagementUserCommand(request);
        var result = await sender.Send(command, cancellationToken);

        if (!result.IsSuccess || result.Value == null)
            return Results.NotFound("User not found");

        return Results.Ok(result.Value);
    }
}