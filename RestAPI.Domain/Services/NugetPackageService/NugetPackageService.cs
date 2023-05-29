using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using ICSharpCode.SharpZipLib.Zip;
using RestAPI.Domain.Services.NugetPackageService.Dtos;

namespace RestAPI.Domain.Services.NugetPackageService;

public class NugetPackageService : INugetPackageService
{
    private const string SemVerRegex =
        "^(0|[1-9]\\d*)\\.(0|[1-9]\\d*)\\.(0|[1-9]\\d*)(?:-((?:0|[1-9]\\d*|\\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\\.(?:0|[1-9]\\d*|\\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\\+([0-9a-zA-Z-]+(?:\\.[0-9a-zA-Z-]+)*))?$";
    
    public NugetValidationResultDto ValidateNugetPackage(MemoryStream nugetPackageStream)
    {
        using var zip = new ZipFile(nugetPackageStream, true);

        foreach (ZipEntry entry in zip)
        {
            if (!entry.IsFile)
                continue;

            if (!entry.Name.EndsWith(".nuspec", StringComparison.OrdinalIgnoreCase))
                continue;

            using var entryReader = new StreamReader(zip.GetInputStream(entry));

            try
            {
                var nugetPackageMetadata = new NugetPackageMetadataDto(entryReader);
                
                return new NugetValidationResultDto
                {
                    IsValid = true,
                    PackageMetadata = nugetPackageMetadata
                };
            }
            catch (Exception e)
            {
                return new NugetValidationResultDto
                {
                    IsValid = false,
                    Error = "Failed to serialize XML",
                    Exception = e
                };
            }
        }

        return new NugetValidationResultDto
        {
            IsValid = false,
            Error = "*.nuspec was not found"
        };
    }

    public MetadataValidationResultDto ValidateMetadata(NugetPackageMetadataDto metadata)
    {
        var result = new MetadataValidationResultDto();

        if (string.IsNullOrWhiteSpace(metadata.Id))
            result.Errors.Add("Package id is missing");

        if (string.IsNullOrWhiteSpace(metadata.Version))
            result.Errors.Add("Package version is missing");
        
        if(!Regex.Matches(metadata.Version!, SemVerRegex).Any())
            result.Errors.Add("Package version is invalid");
        
        if(string.IsNullOrWhiteSpace(metadata.Description))
            result.Errors.Add("Package description is missing");
        
        if(string.IsNullOrWhiteSpace(metadata.Authors))
            result.Errors.Add("Package authors is missing");

        if (!result.Errors.Any())
            result.IsValid = true;

        return result;
    }
}