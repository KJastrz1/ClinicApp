using ClinicApp.Application.Actions.Doctors.Command.AddDoctorsSchedules;
using ClinicApp.Application.Actions.Doctors.Command.DeleteDoctorSchedule;
using ClinicApp.Domain.Shared;
using ClinicApp.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Doctor.Requests;

namespace ClinicApp.Presentation.Controllers;

[Route("api/doctors/{doctorId}/schedules")]
public sealed class DoctorSchedulesController : ApiController
{
    public DoctorSchedulesController(ISender sender)
        : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateDoctorSchedule(
        Guid doctorId,
        [FromBody] List<CreateDoctorScheduleRequest> request,
        CancellationToken cancellationToken)
    {
        var command = new AddDoctorSchedulesCommand(
            doctorId,
            request);

        Result<Guid> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok();
    }

    [HttpDelete("{scheduleId:int}")]
    public async Task<IActionResult> DeleteDoctorSchedule(Guid doctorId, int scheduleId, CancellationToken cancellationToken)
    {
        var command = new DeleteDoctorScheduleCommand(doctorId, scheduleId);

        Result result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }
}
