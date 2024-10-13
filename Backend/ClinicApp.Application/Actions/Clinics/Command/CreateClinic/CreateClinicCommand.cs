using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.Actions.Clinics.Command.CreateClinic;

public sealed record CreateClinicCommand(
    string PhoneNumber,
    string Address,
    string City,
    string ZipCode) : ICommand<Guid>;
