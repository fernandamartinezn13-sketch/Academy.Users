using Academy.Users.Application;
using Academy.Users.Domain.Exceptions;
using Academy.Users.Infrastructure;
using Academy.Users.Infrastructure.Auth;
using Academy.Users.Presentation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

// Capas
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// AuthN / AuthZ
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        var jwt = new JwtSettings();
        builder.Configuration.GetSection(JwtSettings.SectionName).Bind(jwt);

        o.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
        };
    });

builder.Services.AddAuthorization();    // <-- necesario

builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Academy.Users.API",
        Version = "v1"
    });

    // (opcional) botón Authorize
    var jwtScheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Bearer {token}",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new Microsoft.OpenApi.Models.OpenApiReference
        {
            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    c.AddSecurityDefinition("Bearer", jwtScheme);
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        { jwtScheme, Array.Empty<string>() }
    });
});

//PARA EL TOKEN ES LO ANTERIOR
var app = builder.Build();

if (Environment.GetEnvironmentVariable("DEBUG_ENABLE_TEST_ENDPOINTS") == "1")
{
    app.MapTestEndpoints();
}

//PARA PRUEBA LOCAL 
// justo después de var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<Academy.Users.Infrastructure.Persistence.UsersDbContext>();
    var hasher = scope.ServiceProvider.GetRequiredService<Academy.Users.Domain.Abstractions.IPasswordHasher>();

    if (!db.Users.Any(u => u.Email == "aline-local@example.com"))
    {
        var user = new Academy.Users.Domain.Users.Entities.User(
            "Aline", "García", "aline-local@example.com", hasher.Hash("Pa$$w0rd!")
        );
        db.Add(user);
        db.SaveChanges();
    }
}

//Termina para prueba local

app.UseSwagger();
//app.UseSwaggerUI();
app.UseSwaggerUI(c =>
{
    // Swagger UI usará el documento v1 que registraste en AddSwaggerGen
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Academy.Users.API v1");
    // opcional: c.RoutePrefix = "swagger";
});

app.UseExceptionHandler(a => a.Run(async ctx =>
{
    var ex = ctx.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>()?.Error;
    if (ex is InvalidCredentialsException)
    {
        ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
        await ctx.Response.WriteAsJsonAsync(new { message = ex.Message });
    }
    else
    {
        ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await ctx.Response.WriteAsJsonAsync(new { message = "Internal Server Error" });
    }
}));

app.UseAuthentication();
app.UseAuthorization();

app.MapUsersEndpoints();   // o app.MapUsersModule();

app.Run();
