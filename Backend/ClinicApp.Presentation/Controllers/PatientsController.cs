using ClinicApp.Application.UseCases.Patients.Command.CreatePatient;
using ClinicApp.Application.UseCases.Patients.Command.DeletePatient;
using ClinicApp.Application.UseCases.Patients.Command.UpdatePatient;
using ClinicApp.Application.UseCases.Patients.Query.GetPatientById;
using ClinicApp.Application.UseCases.Patients.Query.GetPatients;
using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Shared;
using ClinicApp.Infrastructure.Authentication;
using ClinicApp.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;
using Shared.Contracts.Patient;
using Shared.Contracts.Patient.Requests;
using Shared.Contracts.Patient.Responses;
using Shared.Contracts.Shared;

namespace ClinicApp.Presentation.Controllers;

[Route("api/patients")]
public sealed class PatientsController : ApiController
{
    public PatientsController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPatientById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetPatientByIdQuery(id);

        Result<PatientResponse> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpGet]
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

        Result<PagedItems<PatientResponse>> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }

    [HttpPost]
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


    [HttpPatch("{id:guid}")]
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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePatient(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeletePatientCommand(id);

        Result result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }
}
