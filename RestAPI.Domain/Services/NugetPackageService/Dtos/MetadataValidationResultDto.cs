namespace RestAPI.Domain.Services.NugetPackageService.Dtos;

public class MetadataValidationResultDto
{
    public bool IsValid { get; set; }
    public List<string> Errors { get; set; } = new();
}