using System.Xml.Serialization;

namespace RestAPI.Domain.Services.NugetPackageService.Dtos;

[XmlRoot(ElementName = "package", Namespace = "http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd")]
public class NugetPackageDto
{
    [XmlElement(ElementName = "metadata")]
    public NugetPackageMetadataDto? Metadata { get; set; }
}