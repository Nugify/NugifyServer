using Microsoft.Extensions.Options;
using RestAPI.Domain.Configuration;

namespace RestAPI.Domain.Services.NugetStorageService;

public class NugetFileSystemStorageService : INugetStorageService
{
    private readonly NugifyConfiguration _configuration;

    public NugetFileSystemStorageService(IOptions<NugifyConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }

    public async Task<bool> SavePackage(Guid nugetPackageId, MemoryStream nugetPackageStream)
    {
        var path = Path.Join(_configuration.FileSystem.PackagePath, nugetPackageId.ToString());

        try
        {
            await using var fileStream = new FileStream(path, FileMode.CreateNew);
            nugetPackageStream.WriteTo(fileStream);
            await fileStream.FlushAsync();

            return true;
        }
        catch (IOException ioException)
        {
            // TODO: logging or response for user
            return false;
        }
    }
}