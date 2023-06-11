using System.ComponentModel.DataAnnotations;

namespace RestAPI.Persistence.Models;

public class NugetPackage
{
    [Key]
    public Guid Id { get; set; }

    public string NugetPackageId { get; set; } = null!;
    public string Version { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Authors { get; set; } = null!;
}