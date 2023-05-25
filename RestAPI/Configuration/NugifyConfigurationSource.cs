namespace RestAPI.Configuration;

public class NugifyConfigurationSource : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new NugifyConfigurationProvider();
    }
}