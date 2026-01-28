using OpticaApi.Application.Services;
using OpticaApi.Domain.Repositories;
using OpticaApi.Infrastructure.Database;
using OpticaApi.Infrastructure.Repositories;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

const string connectionString =
    @"Server=(localdb)\MSSQLLocalDB;
      Database=OpticaApi;
      Trusted_Connection=True;";

builder.Services.AddSingleton<DatabaseInitializer>(
    _ => new DatabaseInitializer(connectionString)
);

builder.Services.AddScoped<IClienteRepository>(
    _ => new ClienteRepository(connectionString)
);

builder.Services.AddScoped<IGrauLenteRepository>(
    _ => new GrauLenteRepository(connectionString)
);

builder.Services.AddScoped<IServicoRepository>(
    _ => new ServicoRepository(connectionString)
);

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IGrauLenteService, GrauLenteService>();
builder.Services.AddScoped<IServicoService, ServicoService>();
builder.Services.AddScoped<IPainelService, PainelService>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
#if DEBUG
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader();
#else
        policy
            .WithOrigins("http://localhost:8080")
            .AllowAnyMethod()
            .AllowAnyHeader();
#endif
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbInitializer =
        scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();

    await dbInitializer.InitializeAsync();

    var migrator = new DatabaseMigrator(connectionString);
    await migrator.MigrateAsync();
}

app.UseRouting();
app.UseCors("AllowReact");
app.MapControllers();

app.Run();
