using Academy.Users.Application;
using Academy.Users.Infrastructure;
using Academy.Users.Presentation.Modules;

var builder = WebApplication.CreateBuilder(args);
 
// Agregar servicios de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add application and infrastructure layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
 
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
 
//app.MapGet("/", () => "Academy.Products API - .NET 8");
 
app.Run();