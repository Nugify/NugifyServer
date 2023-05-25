using Microsoft.Extensions.Options;
using RestAPI.Configuration;
using RestAPI.Domain.Services;
using RestAPI.Domain.Services.NugetPackageService;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", true, false)
    .Add(new NugifyConfigurationSource());
builder.Services.Configure<NugifyConfiguration>(builder.Configuration.GetSection("NUGIFY"));

builder.Services.AddControllers();

builder.Services.AddScoped<INugetPackageService, NugetPackageService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();