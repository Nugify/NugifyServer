using System.Xml.Serialization;

namespace RestAPI.Domain.Services.NugetPackageService.Dtos;

public class NugetPackageMetadataDto
{
    [XmlElement(ElementName = "id")]
    public string Id { get; set; }

    [XmlElement(ElementName = "version")]
    public string Version { get; set; }

    [XmlElement(ElementName = "description")]
    public string Description { get; set; }

    [XmlElement(ElementName = "authors")]
    public string Authors { get; set; }
}