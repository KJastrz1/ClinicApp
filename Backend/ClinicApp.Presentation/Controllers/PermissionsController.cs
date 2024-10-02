using ClinicApp.Application.Actions.Roles.Query.GetPermissions;
using ClinicApp.Domain.Shared;
using ClinicApp.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Role.Requests;
using Shared.Contracts.Role.Responses;

namespace ClinicApp.Presentation.Controllers;

[Route("api/permissions")]
public sealed class PermissionsController : ApiController
{
    public PermissionsController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPermissions(
        [FromQuery] PermissionFilter filter,
        CancellationToken cancellationToken)
    {
        var query = new GetPermissionsQuery
        {
            Filter = filter
        };

        Result<List<PermissionResponse>> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }
}
