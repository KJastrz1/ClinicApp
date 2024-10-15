using ClinicApp.Application.Abstractions.Messaging;


namespace ClinicApp.Application.Actions.Doctors.Command.DeleteDoctorSchedule;

public sealed record DeleteDoctorScheduleCommand(Guid DoctorId, int ScheduleId) :  ICommand;
