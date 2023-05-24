using RestAPI.Domain.Services;
using RestAPI.Domain.Services.NugetPackageService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<INugetPackageService, NugetPackageService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();