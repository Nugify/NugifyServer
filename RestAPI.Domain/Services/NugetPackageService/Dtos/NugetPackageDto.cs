using System.Xml.Serialization;

namespace RestAPI.Domain.Services.NugetPackageService.Dtos;

[Serializable, XmlRoot("package", IsNullable = true)]
public class NugetPackageDto
{
    [XmlElement(ElementName = "metadata")] public NugetPackageMetadataDto? Metadata { get; set; }
}