// OpticaApi.WebApi/Program.cs
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using OpticaApi.Application.Services;
using OpticaApi.Application.Validators;
using OpticaApi.Domain.Repositories;
using OpticaApi.Infrastructure.Database;
using OpticaApi.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Database
var connectionString = "Data Source=optica.db";
builder.Services.AddSingleton<DatabaseInitializer>(_ => new DatabaseInitializer(connectionString));

// Repositories
builder.Services.AddScoped<IClienteRepository>(_ => new ClienteRepository(connectionString));
builder.Services.AddScoped<IGrauLenteRepository>(_ => new GrauLenteRepository(connectionString));
builder.Services.AddScoped<IServicoRepository>(_ => new ServicoRepository(connectionString));

// Services
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IGrauLenteService, GrauLenteService>();
builder.Services.AddScoped<IServicoService, ServicoService>();

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateClienteValidator>();

builder.Services.AddControllers();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Ótica CRUD API",
        Version = "v1",
        Description = "API completa para gerenciamento de ótica com cadastro de clientes, graus de lentes e serviços",
        Contact = new OpenApiContact
        {
            Name = "Suporte Ótica CRUD",
            Email = "suporte@opticacrud.com"
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    c.EnableAnnotations();
});

var app = builder.Build();

// Initialize Database
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
    await dbInitializer.InitializeAsync();
}

// Swagger sempre disponível
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ótica CRUD API v1");
    c.RoutePrefix = app.Environment.IsDevelopment() ? "swagger" : "api-docs";
    c.DocumentTitle = "Ótica CRUD API Documentation";
    c.DefaultModelsExpandDepth(2);
    c.DisplayRequestDuration();
    c.EnableDeepLinking();
    c.EnableFilter();
});

app.UseCors("AllowReact");
app.UseRouting();
app.MapControllers();

// Serve React app
if (Directory.Exists("wwwroot"))
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.MapFallbackToFile("index.html");
}

app.Run("http://localhost:5000");