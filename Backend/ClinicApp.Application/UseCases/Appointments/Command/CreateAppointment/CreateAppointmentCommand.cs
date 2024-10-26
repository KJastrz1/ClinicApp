using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.UseCases.Appointments.Command.CreateAppointment;

public sealed record CreateAppointmentCommand(
    Guid DoctorId,
    Guid PatientId,
    Guid ClinicId,
    DateTime StartDateTime,
    string? Notes) : ICommand<Guid>;
