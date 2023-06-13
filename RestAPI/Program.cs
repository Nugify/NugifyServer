using Microsoft.EntityFrameworkCore;
using RestAPI.Domain.Services;
using RestAPI.Domain.Services.NugetPackageService;
using RestAPI.Domain.Services.NugetStorageService;
using RestAPI.Persistence;
using RestAPI.Persistence.Configuration;
using RestAPI.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", true, false)
    .Add(new NugifyConfigurationSource());
builder.Services.Configure<NugifyConfiguration>(builder.Configuration.GetSection("NUGIFY"));

builder.Services.AddControllers();

builder.Services.AddScoped<INugetPackageService, NugetPackageService>();
builder.Services.AddScoped<INugetStorageService, NugetFileSystemStorageService>();
builder.Services.AddPersistence();

var app = builder.Build();

app.MapControllers();

var context = app.Services.GetRequiredService<NugifyContext>();
await context.Database.MigrateAsync();
await context.DisposeAsync();

app.Run();