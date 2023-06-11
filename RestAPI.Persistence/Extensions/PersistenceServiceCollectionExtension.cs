using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RestAPI.Persistence.Extensions;

public static class PersistenceServiceCollectionExtension
{
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<NugifyContext>();
    }
}