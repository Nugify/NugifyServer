namespace RestAPI.Persistence.Configuration;

public class NugifyConfiguration
{
    public NugifyFileSystemConfiguration FileSystem { get; set; } = new();
    public NugifyDatabaseConfiguration? Database { get; set; }
}