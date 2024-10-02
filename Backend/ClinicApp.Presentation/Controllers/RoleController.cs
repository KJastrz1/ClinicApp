using ClinicApp.Application.Actions.Roles.Command.CreateRole;
using ClinicApp.Application.Actions.Roles.Query.GetRoleByIdWithPermissions;
using ClinicApp.Application.Actions.Roles.Query.GetPermissions;
using ClinicApp.Application.Actions.Roles.Query.GetRoleById;
using ClinicApp.Domain.Shared;
using ClinicApp.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Role;
using Shared.Contracts.Role.Requests;
using Shared.Contracts.Role.Responses;

namespace ClinicApp.Presentation.Controllers;

[Route("api/roles")]
public sealed class RolesController : ApiController
{
    public RolesController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRoleByIdWithPermissions(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetRoleByIdQuery(id);

        Result<RoleResponse> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(
        [FromBody] CreateRoleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateRoleCommand(
            request.Name,
            request.Permissions 
        );

        Result<Guid> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return CreatedAtAction(
            nameof(GetRoleByIdWithPermissions),
            new { id = result.Value },
            result.Value);
    }

    [HttpGet("permissions")]
    public async Task<IActionResult> GetPermissions(CancellationToken cancellationToken)
    {
        var query = new GetPermissionsQuery();

        Result<List<PermissionResponse>> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }
}
