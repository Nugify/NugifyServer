using Microsoft.Extensions.Options;
using RestAPI.Domain.Services.NugetPackageService.Dtos;
using RestAPI.Persistence.Configuration;
using RestAPI.Persistence.Repositories;

namespace RestAPI.Domain.Services.NugetStorageService;

public class NugetFileSystemStorageService : INugetStorageService
{
    private readonly NugifyConfiguration _configuration;
    private readonly INugetPackageRepository _nugetPackageRepository;

    public NugetFileSystemStorageService(IOptions<NugifyConfiguration> configuration,
        INugetPackageRepository nugetPackageRepository)
    {
        _nugetPackageRepository = nugetPackageRepository;
        _configuration = configuration.Value;
    }

    public async Task<bool> SavePackage(NugetPackageMetadataDto metadataDto, MemoryStream nugetPackageStream)
    {
        var nugetPackageId = await _nugetPackageRepository.Insert(metadataDto.Id!, metadataDto.Version!,
            metadataDto.Description!, metadataDto.Authors!);
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