using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestAPI.Persistence.Configuration;

namespace RestAPI.Persistence;

public class NugifyContext : DbContext
{
    private readonly NugifyConfiguration _configuration;

    public NugifyContext(IOptions<NugifyConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.Database!.ConnectionString);
    }
}