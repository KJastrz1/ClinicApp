using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.UseCases.Clinics.Command.UpdateClinic;

public sealed record UpdateClinicCommand(
    Guid ClinicId,
    string? PhoneNumber,
    string? Address,
    string? City,
    string? ZipCode) : ICommand;
