using ClinicApp.Application.Actions.Doctors.Command.CreateDoctor;
using ClinicApp.Application.Actions.Doctors.Command.DeleteDoctor;
using ClinicApp.Application.Actions.Doctors.Command.UpdateDoctor;
using ClinicApp.Application.Actions.Doctors.Query.GetDoctorById;
using ClinicApp.Application.Actions.Doctors.Query.GetDoctors;
using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Shared;
using ClinicApp.Infrastructure.Authentication;
using ClinicApp.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;
using Shared.Contracts.Doctor;
using Shared.Contracts.Doctor.Requests;
using Shared.Contracts.Doctor.Responses;

namespace ClinicApp.Presentation.Controllers;

[Route("api/doctors")]
public sealed class DoctorsController : ApiController
{
    public DoctorsController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetDoctorById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetDoctorByIdQuery(id);

        Result<DoctorResponse> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpGet]
    public async Task<IActionResult> GetDoctors(
        [FromQuery] DoctorFilter filter,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var query = new GetDoctorsQuery
        {
            Filter = filter,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        Result<PagedItems<DoctorResponse>> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDoctor(
        [FromBody] CreateDoctorRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateDoctorCommand(
            request.FirstName,
            request.LastName,
            request.MedicalLicenseNumber,
            request.Specialties,
            request.Bio,
            request.AcademicTitle,
            request.AccountId,
            request.ClinicId);

        Result<Guid> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return CreatedAtAction(
            nameof(GetDoctorById),
            new { id = result.Value },
            result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateDoctor(
        Guid id,
        [FromBody] UpdateDoctorRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateDoctorCommand(
            id,
            request.FirstName,
            request.LastName,
            request.MedicalLicenseNumber,
            request.Specialties,
            request.Bio,
            request.AcademicTitle,
            request.ClinicId);

        Result result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteDoctor(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteDoctorCommand(id);

        Result result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }
    
    
}
