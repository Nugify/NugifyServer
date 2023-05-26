namespace RestAPI.Domain.Services;

public interface INugetStorageService
{
    Task<bool> SavePackage(Guid nugetPackageId, MemoryStream nugetPackageStream);
}