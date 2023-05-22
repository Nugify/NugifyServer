using Microsoft.AspNetCore.Mvc;

namespace RestAPI.Controllers.Nuget;

[Route("/api/v2/publish")]
public class PublishPackageController : BaseController
{
    [HttpPut]
    public async Task<IActionResult> PublishPackage(CancellationToken cancellationToken)
    {
        //TODO: authorization

        //TODO: check if request is valid
        if (!Request.HasFormContentType)
            return BadRequest();

        var form = await Request.ReadFormAsync(cancellationToken);

        if (form.Files.Count < 1)
            return BadRequest();

        // TODO: check if package is valid

        //TODO: check if package does not exist

        //TODO: save package

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