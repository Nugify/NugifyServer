namespace RestAPI.Domain.Services.NugetPackageService.Dtos;

public class NugetValidationResultDto
{
    public NugetPackageMetadataDto? PackageMetadata { get; set; }
    public bool IsValid { get; set; }
    public string? Error { get; set; }
    public Exception? Exception { get; set; }
}