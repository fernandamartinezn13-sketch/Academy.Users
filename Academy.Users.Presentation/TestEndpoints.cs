using Academy.Users.Domain.Abstractions;
using Academy.Users.Domain.Users.Entities;
using Academy.Users.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Academy.Users.Presentation;

public static class TestEndpoints
{
    /// <summary>
    /// Endpoints TEMPORALES para verificar conexión/lectura/escritura.
    /// ¡No dejarlos habilitados en producción!
    /// </summary>
    public static IEndpointRouteBuilder MapTestEndpoints(this IEndpointRouteBuilder app)
    {
        var grp = app.MapGroup("/_test")
            .WithTags("Test (TEMP)")
            .WithDescription("Endpoints temporales para ver/insertar usuarios. No exponen PasswordHash.");

        // GET /_test/users/count  -> { count: n }
        grp.MapGet("/users/count", async (UsersDbContext db) =>
        {
            var count = await db.Users.CountAsync();
            return Results.Ok(new { count });
        });

        // GET /_test/users?take=20&skip=0&search=aline
        grp.MapGet("/users", async (UsersDbContext db, int? take, int? skip, string? search) =>
        {
            var query = db.Users.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(u =>
                    u.Email.Contains(search) ||
                    u.FirstName.Contains(search) ||
                    u.LastName.Contains(search));
            }

            var total = await query.CountAsync();

            var data = await query
                .OrderBy(u => u.Email)
                .Skip(Math.Max(0, skip ?? 0))
                .Take(Math.Clamp(take ?? 50, 1, 200))
                .Select(u => new UserDto(u.Id, u.FirstName, u.LastName, u.Email, u.IsActive, u.IsBlocked))
                .ToListAsync();

            return Results.Ok(new { total, returned = data.Count, users = data });
        });

        // POST /_test/users  -> inserta usuario de prueba
        grp.MapPost("/users", async (UsersDbContext db, IPasswordHasher hasher, CreateTestUserRequest req) =>
        {
            // Validación simple
            var problems = Validate(req);
            if (problems is not null) return Results.ValidationProblem(problems);

            if (await db.Users.AnyAsync(u => u.Email == req.Email))
                return Results.BadRequest(new { message = "Email already exists" });

            var user = new User(
                req.FirstName.Trim(),
                req.LastName.Trim(),
                req.Email.Trim(),
                hasher.Hash(req.Password!)
            );

            db.Add(user);
            await db.SaveChangesAsync();

            var dto = new UserDto(user.Id, user.FirstName, user.LastName, user.Email, user.IsActive, user.IsBlocked);
            return Results.Created($"/_test/users/by-id/{user.Id}", dto);
        });

        return app;
    }

    // Helpers
    private static Dictionary<string, string[]>? Validate(CreateTestUserRequest r)
    {
        var dict = new Dictionary<string, string[]>();

        void add(string k, string v)
        {
            if (!dict.TryGetValue(k, out var arr))
                dict[k] = new[] { v };
            else
                dict[k] = arr.Concat(new[] { v }).ToArray();
        }

        if (string.IsNullOrWhiteSpace(r.FirstName)) add(nameof(r.FirstName), "FirstName is required");
        if (string.IsNullOrWhiteSpace(r.LastName)) add(nameof(r.LastName), "LastName is required");
        if (string.IsNullOrWhiteSpace(r.Email)) add(nameof(r.Email), "Email is required");
        else if (!new EmailAddressAttribute().IsValid(r.Email)) add(nameof(r.Email), "Invalid email");
        if (string.IsNullOrWhiteSpace(r.Password)) add(nameof(r.Password), "Password is required");
        else if (r.Password!.Length < 6) add(nameof(r.Password), "Min length: 6");

        return dict.Count == 0 ? null : dict;
    }

    // DTOs y requests (sin PasswordHash)
    public sealed record UserDto(Guid Id, string FirstName, string LastName, string Email, bool IsActive, bool IsBlocked);

    public sealed class CreateTestUserRequest
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string? Password { get; set; } = null;
    }
}
