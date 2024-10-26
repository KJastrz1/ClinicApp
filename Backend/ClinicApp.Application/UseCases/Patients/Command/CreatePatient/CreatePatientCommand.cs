using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.UseCases.Patients.Command.CreatePatient;

public sealed record CreatePatientCommand(
    string FirstName,
    string LastName,
    string SocialSecurityNumber,
    DateOnly DateOfBirth) : ICommand<Guid>;
