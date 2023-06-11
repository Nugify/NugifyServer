using RestAPI.Persistence.Models;

namespace RestAPI.Persistence.Repositories;

public interface INugetPackageRepository
{
    Task<Guid> Insert(string nugetPackageId, string version, string description, string authors);
    Task Delete(Guid nugetPackageId);
    Task<List<NugetPackage>> GetAll();
    Task<NugetPackage?> GetById(Guid nugetPackageId);
}