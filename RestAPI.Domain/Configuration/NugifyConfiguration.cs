namespace RestAPI.Domain.Configuration;

public class NugifyConfiguration
{
    public NugifyFileSystemConfiguration FileSystem { get; set; } = new();
}