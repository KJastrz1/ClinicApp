using ClinicApp.Application.Actions.Clinics.Command.CreateClinic;
using ClinicApp.Application.Actions.Clinics.Command.DeleteClinic;
using ClinicApp.Application.Actions.Clinics.Command.UpdateClinic;
using ClinicApp.Application.Actions.Clinics.Query.GetClinicById;
using ClinicApp.Application.Actions.Clinics.Query.GetClinics;
using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Shared;
using ClinicApp.Infrastructure.Authentication;
using ClinicApp.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;
using Shared.Contracts.Clinic;
using Shared.Contracts.Clinic.Requests;
using Shared.Contracts.Clinic.Responses;

namespace ClinicApp.Presentation.Controllers;

[Route("api/clinics")]
public sealed class ClinicsController : ApiController
{
    public ClinicsController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet("{id:guid}")]
    [HasPermission(PermissionEnum.ReadClinic)]
    public async Task<IActionResult> GetClinicById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetClinicByIdQuery(id);

        Result<ClinicResponse> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpGet]
    [HasPermission(PermissionEnum.ReadClinic)]
    public async Task<IActionResult> GetClinics(
        [FromQuery] ClinicFilter filter,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var query = new GetClinicsQuery
        {
            Filter = filter,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        Result<PagedItems<ClinicResponse>> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }

    [HttpPost]
    [HasPermission(PermissionEnum.CreateClinic)]
    public async Task<IActionResult> CreateClinic(
        [FromBody] CreateClinicRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateClinicCommand(
            request.PhoneNumber,
            request.Address,
            request.City,
            request.ZipCode);

        Result<Guid> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return CreatedAtAction(
            nameof(GetClinicById),
            new { id = result.Value },
            result.Value);
    }

    [HttpPut("{id:guid}")]
    [HasPermission(PermissionEnum.UpdateClinic)]
    public async Task<IActionResult> UpdateClinic(
        Guid id,
        [FromBody] UpdateClinicRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateClinicCommand(
            id,
            request.PhoneNumber,
            request.Address,
            request.City,
            request.ZipCode);

        Result result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [HasPermission(PermissionEnum.DeleteClinic)]
    public async Task<IActionResult> DeleteClinic(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteClinicCommand(id);

        Result result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }
}