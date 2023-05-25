using RestAPI.Domain.Services.NugetPackageService.Dtos;

namespace RestAPI.Domain.Services;

public interface INugetPackageService
{
    NugetValidationResultDto ValidateNugetPackage(MemoryStream nugetPackageStream);
    MetadataValidationResultDto ValidateMetadata(NugetPackageMetadataDto metadata);
}