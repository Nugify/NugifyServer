using System.Xml.Serialization;

namespace RestAPI.Domain.Services.NugetPackageService.Dtos;

[Serializable, XmlRoot("package", Namespace = "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd", IsNullable = true)]
public class NugetPackageDto
{
    [XmlElement(ElementName = "metadata")] public NugetPackageMetadataDto? Metadata { get; set; }
}