using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.Actions.Clinics.Command.DeleteClinic;

public sealed record DeleteClinicCommand(
    Guid ClinicId) : ICommand<Guid>;
