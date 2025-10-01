using Academy.Users.Application;
using Academy.Users.Application.Users.Commands.UserManagement;
using Academy.Users.Domain.Entities.UserEntity.Repositories;
using Academy.Users.Infrastructure.Persistence.Context;
using Academy.Users.Infrastructure.Persistence.Repositories;
using Academy.Users.Presentation.Modules;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
 
// Agregar servicios de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
//builder.Services.addInfrastructureService();

// Aquí puedes registrar tus servicios de Application, Infrastructure, etc.
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(ManagementUserCommandHandler).Assembly));

var app = builder.Build();
 
// Configurar Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Academy.Products API v1");
        //options.RoutePrefix = string.Empty; // Para que se muestre en la raíz: https://localhost:5001/
    });
}

ModulesConfiguration.Configure(app);

app.UseHttpsRedirection();
 
app.Run();