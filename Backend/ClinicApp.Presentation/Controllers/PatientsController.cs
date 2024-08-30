using ClinicApp.Application.Features.Patient.CreatePatient;
using ClinicApp.Application.Features.Patient.GetPatientById;
using ClinicApp.Domain.Shared;
using ClinicApp.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> GetPatientById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetPatientByIdQuery(id);
    
        Result<PatientResponse> response = await Sender.Send(query, cancellationToken);
    
        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterPatient(
        [FromBody] CreatePatientRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreatePatientCommand(
            request.Email,
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

    // [HasPermission(Permission.UpdatePatient)]
    // [HttpPut("{id:guid}")]
    // public async Task<IActionResult> UpdatePatient(
    //     Guid id,
    //     [FromBody] UpdatePatientRequest request,
    //     CancellationToken cancellationToken)
    // {
    //     var command = new UpdatePatientCommand(
    //         id,
    //         request.FirstName,
    //         request.LastName,
    //         request.SocialSecurityNumber,
    //         request.DateOfBirth);
    //
    //     Result result = await Sender.Send(
    //         command,
    //         cancellationToken);
    //
    //     if (result.IsFailure)
    //     {
    //         return HandleFailure(result);
    //     }
    //
    //     return NoContent();
    // }
}
