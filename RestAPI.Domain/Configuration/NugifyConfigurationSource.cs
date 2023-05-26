using Microsoft.Extensions.Configuration;

namespace RestAPI.Domain.Configuration;

public class NugifyConfigurationSource : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new NugifyConfigurationProvider();
    }
}