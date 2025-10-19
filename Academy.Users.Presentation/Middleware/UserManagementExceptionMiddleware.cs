using Academy.Users.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Academy.Users.Presentation.Middleware;

public class UserManagementExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public UserManagementExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (InvalidRequestDataException ex)
        {
            await WriteAsync(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (InvalidStatusException ex)
        {
            await WriteAsync(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (UserAlreadyInStateException ex)
        {
            await WriteAsync(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (BadHttpRequestException ex)
        {
            string message = "Utilice true para activar y false para bloquear";
            await WriteAsync(context, HttpStatusCode.BadRequest, message);
        }
        catch
        {
            await WriteAsync(context, HttpStatusCode.InternalServerError, "Error en el servidor");
        }
    }

    private static async Task WriteAsync(HttpContext ctx, HttpStatusCode code, string message)
    {
        ctx.Response.ContentType = "application/json";
        ctx.Response.StatusCode = (int)code;

        var payload = new { status = (int)code, error = message };
        await ctx.Response.WriteAsync(JsonSerializer.Serialize(payload));
    }
}
