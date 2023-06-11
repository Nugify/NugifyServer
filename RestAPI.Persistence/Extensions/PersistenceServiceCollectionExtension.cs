using Microsoft.Extensions.DependencyInjection;
using RestAPI.Persistence.Repositories;
using RestAPI.Persistence.Repositories.NugetPackageRepository;

namespace RestAPI.Persistence.Extensions;

public static class PersistenceServiceCollectionExtension
{
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<NugifyContext>();

        services.AddScoped<INugetPackageRepository, NugetPackageRepository>();
    }
}