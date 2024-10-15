using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts.Doctor.Requests;

namespace ClinicApp.Application.Actions.Doctors.Command.AddDoctorsSchedules;

public sealed record AddDoctorSchedulesCommand(
    Guid DoctorId,
    List<CreateDoctorScheduleRequest>? Schedules
) : ICommand<Guid>;
