using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.UseCases.Clinics.Command.DeleteClinic;

public sealed record DeleteClinicCommand(
    Guid ClinicId) : ICommand;
