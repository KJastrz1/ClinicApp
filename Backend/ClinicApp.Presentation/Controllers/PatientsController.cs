using ClinicApp.Application.Actions.Patients.Command.CreatePatient;
using ClinicApp.Application.Actions.Patients.Command.RegisterPatient;
using ClinicApp.Application.Actions.Patients.Command.UpdatePatient;
using ClinicApp.Application.Actions.Patients.Query.GetPatientById;
using ClinicApp.Application.Actions.Patients.Query.GetPatients;
using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Shared;
using ClinicApp.Infrastructure.Authentication;
using ClinicApp.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;
using Shared.Contracts.Patient;

namespace ClinicApp.Presentation.Controllers;

[Route("api/patients")]
public sealed class PatientsController : ApiController
{
    public PatientsController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet("{id:guid}")]
    [HasPermission(PermissionEnum.ReadPatient)]
    public async Task<IActionResult> GetPatientById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetPatientByIdQuery(id);

        Result<PatientResponse> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpGet]
    [HasPermission(PermissionEnum.ReadPatient)]
    public async Task<IActionResult> GetPatients(
        [FromQuery] PatientFilter filter,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var query = new GetPatientsQuery
        {
            Filter = filter,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        Result<PagedResult<PatientResponse>> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }


    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterPatient(
        [FromBody] RegisterPatientRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterPatientCommand(
            request.Email,
            request.Password,
            request.FirstName,
            request.LastName,
            request.SocialSecurityNumber,
            request.DateOfBirth);

        Result<Guid> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return CreatedAtAction(
            nameof(GetPatientById),
            new { id = result.Value },
            result.Value);
    }

    [HttpPost]
    [HasPermission(PermissionEnum.CreatePatient)]
    public async Task<IActionResult> CreatePatient(
        [FromBody] CreatePatientRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreatePatientCommand(
            request.FirstName,
            request.LastName,
            request.SocialSecurityNumber,
            request.DateOfBirth);

        Result<Guid> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return CreatedAtAction(
            nameof(GetPatientById),
            new { id = result.Value },
            result.Value);
    }


    [HttpPut("{id:guid}")]
    [HasPermission(PermissionEnum.UpdatePatient)]
    public async Task<IActionResult> UpdatePatient(
        Guid id,
        [FromBody] UpdatePatientRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdatePatientCommand(
            id,
            request.FirstName,
            request.LastName,
            request.SocialSecurityNumber,
            request.DateOfBirth);

        Result result = await Sender.Send(
            command,
            cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }
}
