using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.Actions.Patients.Command.RegisterPatient;

public sealed record RegisterPatientCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string SocialSecurityNumber,
    DateOnly DateOfBirth) : ICommand<Guid>;
