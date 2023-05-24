using RestAPI.Domain.Services.NugetPackageService.Dtos;

namespace RestAPI.Domain.Services;

public interface INugetPackageService
{
    Task<NugetValidationResultDto> ValidateNugetPackage(MemoryStream nugetPackageStream, CancellationToken cancellationToken);
}