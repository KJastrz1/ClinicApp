using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.UseCases.Doctors.Command.DeleteDoctorSchedule;

public sealed record DeleteDoctorScheduleCommand(Guid DoctorId, int ScheduleId) :  ICommand;
