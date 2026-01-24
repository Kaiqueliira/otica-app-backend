using OpticaApi.Application.Services;
using OpticaApi.Domain.Repositories;
using OpticaApi.Infrastructure.Database;
using OpticaApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Services
// --------------------
var connectionString = "Data Source=optica.db";

builder.Services.AddSingleton<DatabaseInitializer>(_ =>
    new DatabaseInitializer(connectionString));

builder.Services.AddScoped<IClienteRepository>(_ =>
    new ClienteRepository(connectionString));
builder.Services.AddScoped<IGrauLenteRepository>(_ =>
    new GrauLenteRepository(connectionString));
builder.Services.AddScoped<IServicoRepository>(_ =>
    new ServicoRepository(connectionString));

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IGrauLenteService, GrauLenteService>();
builder.Services.AddScoped<IServicoService, ServicoService>();
builder.Services.AddScoped<IPainelService, PainelService>();

builder.Services.AddControllers();

// --------------------
// CORS
// --------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy
            .WithOrigins("http://localhost:8080")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// --------------------
// Swagger
// --------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --------------------
// Init DB
// --------------------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
    await db.InitializeAsync();
}

// --------------------
// Pipeline (ORDEM IMPORTA)
// --------------------
app.UseRouting();

app.UseCors("AllowReact");

app.MapControllers();

app.Run();
