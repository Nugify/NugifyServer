using RestAPI.Domain.Services;
using RestAPI.Domain.Services.NugetPackageService;
using RestAPI.Domain.Services.NugetStorageService;
using RestAPI.Persistence.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", true, false)
    .Add(new NugifyConfigurationSource());
builder.Services.Configure<NugifyConfiguration>(builder.Configuration.GetSection("NUGIFY"));

builder.Services.AddControllers();

builder.Services.AddScoped<INugetPackageService, NugetPackageService>();
builder.Services.AddScoped<INugetStorageService, NugetFileSystemStorageService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();