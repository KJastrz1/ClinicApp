using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.UseCases.Appointments.Command.DeleteAppointment;

public sealed record DeleteAppointmentCommand(Guid AppointmentId) : ICommand<Guid>;
