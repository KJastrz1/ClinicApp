using ClinicApp.Application.UseCases.Appointments.Command.CreateAppointment;
using ClinicApp.Application.UseCases.Appointments.Command.DeleteAppointment;
using ClinicApp.Application.UseCases.Appointments.Command.UpdateNotes;
using ClinicApp.Domain.Shared;
using ClinicApp.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Appointment.Requests;

namespace ClinicApp.Presentation.Controllers;

[Route("api/appointments")]
public sealed class AppointmentsController : ApiController
{
    public AppointmentsController(ISender sender)
        : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment(
        [FromBody] CreateAppointmentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateAppointmentCommand(
            request.DoctorId,
            request.PatientId,
            request.ClinicId,
            request.StartDateTime,
            request.Notes);

        Result<Guid> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Created(string.Empty, result.Value);
    }

    [HttpPatch("{id:guid}/notes")]
    public async Task<IActionResult> UpdateNotes(
        Guid id,
        [FromBody] UpdateAppointmentNotesRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateNotesCommand(id, request.Notes);

        Result result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAppointment(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteAppointmentCommand(id);

        Result result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }
}
