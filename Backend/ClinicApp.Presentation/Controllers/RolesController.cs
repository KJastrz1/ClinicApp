using ClinicApp.Application.UseCases.Roles.Command.AddPermissionsToRole;
using ClinicApp.Application.UseCases.Roles.Command.CreateRole;
using ClinicApp.Application.UseCases.Roles.Command.RemovePermissionsFromRole;
using ClinicApp.Application.UseCases.Roles.Command.UpdateRole;
using ClinicApp.Application.UseCases.Roles.Query.GetRoleById;
using ClinicApp.Application.UseCases.Roles.Query.GetRoles;
using ClinicApp.Domain.Shared;
using ClinicApp.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;
using Shared.Contracts.Role.Requests;
using Shared.Contracts.Role.Responses;
using Shared.Contracts.Shared;

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

    [HttpGet]
    public async Task<IActionResult> GetRoles(
        [FromQuery] RoleFilter filter,
        CancellationToken cancellationToken)
    {
        var query = new GetRolesQuery()
        {
            Filter = filter
        };

        Result<PagedItems<RoleResponse>> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(
        [FromBody] CreateRoleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateRoleCommand(
            request.Name,
            request.PermissionsIds
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

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateRole(
        Guid id,
        [FromBody] UpdateRoleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateRoleCommand(
            id,
            request.Name
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

    [HttpPatch("{id:guid}/add-permissions")]
    public async Task<IActionResult> AddPermissionsToRole(
        Guid id,
        [FromBody] AddPermissionsToRoleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddPermissionsToRoleCommand(
            id,
            request.PermissionsIds
        );

        Result<Guid> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }

    [HttpPatch("{id:guid}/remove-permissions")]
    public async Task<IActionResult> RemovePermissionsFromRole(
        Guid id,
        [FromBody] RemovePermissionsFromRoleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RemovePermissionsFromRoleCommand(
            id,
            request.PermissionsIds
        );

        Result<Guid> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }
}
