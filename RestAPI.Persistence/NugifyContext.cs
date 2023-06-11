using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestAPI.Persistence.Configuration;
using RestAPI.Persistence.Models;

namespace RestAPI.Persistence;

public class NugifyContext : DbContext
{
    private readonly NugifyConfiguration _configuration;

    public NugifyContext(IOptions<NugifyConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }
    
    public DbSet<NugetPackage> NugetPackages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }
}