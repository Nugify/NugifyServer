using RestAPI.Domain.Services.NugetPackageService.Dtos;

namespace RestAPI.Domain.Services;

public interface INugetStorageService
{
    Task<bool> SavePackage(NugetPackageMetadataDto metadataDto, MemoryStream nugetPackageStream);
}