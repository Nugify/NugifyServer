using System.Xml;
using System.Xml.Serialization;
using ICSharpCode.SharpZipLib.Zip;
using RestAPI.Domain.Services.NugetPackageService.Dtos;

namespace RestAPI.Domain.Services.NugetPackageService;

public class NugetPackageService : INugetPackageService
{
    public async Task<NugetValidationResultDto> ValidateNugetPackage(MemoryStream nugetPackageStream,
        CancellationToken cancellationToken)
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
}