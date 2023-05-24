using System.Xml.Serialization;

namespace RestAPI.Domain.Services.NugetPackageService.Dtos;

[XmlRoot(ElementName = "metadata")]
public class NugetPackageMetadataDto
{
    [XmlElement(ElementName = "id")] public string? Id { get; set; }

    [XmlElement(ElementName = "version")] public string? Version { get; set; }

    [XmlElement(ElementName = "title")] public string? Title { get; set; }

    [XmlElement(ElementName = "authors")] public string? Authors { get; set; }

    [XmlElement(ElementName = "licenseUrl")]
    public string? LicenseUrl { get; set; }

    [XmlElement(ElementName = "projectUrl")]
    public string? ProjectUrl { get; set; }

    [XmlElement(ElementName = "description")]
    public string? Description { get; set; }

    [XmlElement(ElementName = "tags")] public string? Tags { get; set; }
}