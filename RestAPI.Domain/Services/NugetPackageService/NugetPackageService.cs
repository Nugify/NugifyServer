using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using ICSharpCode.SharpZipLib.Zip;
using RestAPI.Domain.Services.NugetPackageService.Dtos;

namespace RestAPI.Domain.Services.NugetPackageService;

public class NugetPackageService : INugetPackageService
{
    private const string _semVerRegex =
        "^(0|[1-9]\\d*)\\.(0|[1-9]\\d*)\\.(0|[1-9]\\d*)(?:-((?:0|[1-9]\\d*|\\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\\.(?:0|[1-9]\\d*|\\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\\+([0-9a-zA-Z-]+(?:\\.[0-9a-zA-Z-]+)*))?$";
    
    public NugetValidationResultDto ValidateNugetPackage(MemoryStream nugetPackageStream)
    {
        using var zip = new ZipFile(nugetPackageStream);

        foreach (ZipEntry entry in zip)
        {
            if (!entry.IsFile)
                continue;

            if (!entry.Name.EndsWith(".nuspec", StringComparison.OrdinalIgnoreCase))
                continue;

            using var entryReader = new StreamReader(zip.GetInputStream(entry));

            var xmlReader = new XmlTextReader(entryReader);
            var serializer = new XmlSerializer(typeof(NugetPackageDto));
            var nugetPackage = serializer.Deserialize(xmlReader) as NugetPackageDto;

            if (nugetPackage == null || nugetPackage.Metadata == null)
                return new NugetValidationResultDto
                {
                    IsValid = false,
                    Error = "Failed to serialize XML"
                };

            return new NugetValidationResultDto
            {
                IsValid = true,
                PackageMetadata = nugetPackage.Metadata
            };
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
        
        if(!Regex.Matches(metadata.Version!, _semVerRegex).Any())
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