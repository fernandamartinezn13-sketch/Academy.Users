using Academy.Users.Application;
using Academy.Users.Application.Users.Commands.UserManagement;
using Academy.Users.Domain.Entities.UserEntity.Repositories;
using Academy.Users.Infrastructure.Persistence.Context;
using Academy.Users.Infrastructure.Persistence.Repositories;
using Academy.Users.Presentation.Middleware;
using Academy.Users.Presentation.Modules;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(ManagementUserCommandHandler).Assembly));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Academy.Products API v1");
    });
}

app.UseHttpsRedirection();

ModulesConfiguration.Configure(app);

app.UseMiddleware<UserManagementExceptionMiddleware>();

app.Run();