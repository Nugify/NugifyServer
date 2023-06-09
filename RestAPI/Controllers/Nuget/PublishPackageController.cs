using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RestAPI.Domain.Services;

namespace RestAPI.Controllers.Nuget;

[Route("/api/v2/publish")]
public class PublishPackageController : BaseController
{
    private readonly INugetPackageService _nugetPackageService;
    private readonly INugetStorageService _nugetStorageService;

    public PublishPackageController(INugetPackageService nugetPackageService, INugetStorageService nugetStorageService)
    {
        _nugetPackageService = nugetPackageService;
        _nugetStorageService = nugetStorageService;
    }

    [HttpPut]
    public async Task<IActionResult> PublishPackage(CancellationToken cancellationToken)
    {
        //TODO: authorization

        if (!Request.HasFormContentType)
            return BadRequest();

        var form = await Request.ReadFormAsync(cancellationToken);

        if (form.Files.Count < 1)
            return BadRequest();

        var nugetPackage = form.Files.First();

        await using var nugetPackageStream = new MemoryStream();
        await nugetPackage.CopyToAsync(nugetPackageStream, cancellationToken);

        var packageValidationResult = _nugetPackageService.ValidateNugetPackage(nugetPackageStream);
        if (!packageValidationResult.IsValid || packageValidationResult.PackageMetadata == null)
            return new BadRequestObjectResult(packageValidationResult.Error);

        var metadataValidationResult = _nugetPackageService.ValidateMetadata(packageValidationResult.PackageMetadata);
        if (!metadataValidationResult.IsValid)
            return new BadRequestObjectResult(metadataValidationResult.Errors);

        //TODO: check if package does not exist
        
        var packageSaveResult =
            await _nugetStorageService.SavePackage(packageValidationResult.PackageMetadata, nugetPackageStream);

        if (!packageSaveResult)
            return StatusCode(StatusCodes.Status500InternalServerError);

        return new AcceptedResult();
    }

    [HttpDelete("{id}/{version}")]
    public async Task<IActionResult> DeletePackage([FromRoute] string? id, [FromRoute] string? version,
        CancellationToken cancellationToken)
    {
        //TODO: authorization

        //TODO: check if request is valid
        if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(version))
            return new NotFoundResult();

        //TODO: check if data is valid

        //TODO: delete package

        return new NoContentResult();
    }

    [HttpPost("{id}/{version}")]
    public async Task<IActionResult> RelistPackage([FromRoute] string? id, [FromRoute] string? version,
        CancellationToken cancellationToken)
    {
        //TODO: authorization

        //TODO: check if request is valid
        if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(version))
            return new NotFoundResult();

        //TODO: check if data is valid

        //TODO: relist package

        return new OkResult();
    }
}