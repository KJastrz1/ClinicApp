using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.Actions.Clinics.Command.UpdateClinic;

public sealed record UpdateClinicCommand(
    Guid ClinicId,
    string? PhoneNumber,
    string? Address,
    string? City,
    string? ZipCode) : ICommand;
