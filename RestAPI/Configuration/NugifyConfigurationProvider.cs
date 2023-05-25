namespace RestAPI.Configuration;

public class NugifyConfigurationProvider : ConfigurationProvider
{
    public override void Load()
    {
        var environmentVariables = Environment.GetEnvironmentVariables();
        var data = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

        foreach (string envName in environmentVariables.Keys)
        {
            if(!envName.StartsWith("NUGIFY_"))
                continue;

            var key = envName.Replace('_', ':');

            data.Add(key, (string?)environmentVariables[envName]);
        }

        Data = data;
    }
}