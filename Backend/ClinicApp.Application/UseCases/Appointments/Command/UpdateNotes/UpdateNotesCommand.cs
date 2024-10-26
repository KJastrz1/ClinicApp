using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.UseCases.Appointments.Command.UpdateNotes;

public sealed record UpdateNotesCommand(Guid AppointmentId, string Notes) : ICommand<Guid>;
