using Microsoft.Extensions.Configuration;

namespace RestAPI.Persistence.Configuration;

public class NugifyConfigurationSource : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new NugifyConfigurationProvider();
    }
}