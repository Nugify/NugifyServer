using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RestAPI.Domain.Services.NugetPackageService.Dtos;

public class NugetPackageMetadataDto
{
    private XDocument? _document;
    private XElement? _metadataNode;

    public NugetPackageMetadataDto(StreamReader nuspecStream)
    {
        LoadMetadata(nuspecStream);
    }

    public string? Id => GetValue("id");
    public string? Version => GetValue("version");
    public string? Description => GetValue("description");
    public string? Authors => GetValue("authors");

    private string? GetValue(string key)
    {
        if (_document == null || _metadataNode == null)
            return null;

        var node = _metadataNode
            .Elements(XName.Get(key, _metadataNode.GetDefaultNamespace().NamespaceName))
            .FirstOrDefault();

        return node?.Value;
    }

    private void LoadMetadata(StreamReader nuspecStream)
    {
        using var xmlReader = XmlReader.Create(nuspecStream, new XmlReaderSettings
        {
            CloseInput = false,
            IgnoreComments = true,
            IgnoreWhitespace = true,
            IgnoreProcessingInstructions = true
        });
        
        _document = XDocument.Load(xmlReader, LoadOptions.None);

        if (_document.Root == null)
            throw new InvalidDataException("Nuspec stream is invalid");
        
        _metadataNode = _document.Root
            .Elements().FirstOrDefault(x => StringComparer.Ordinal.Equals(x.Name.LocalName, "metadata"));
    }
}