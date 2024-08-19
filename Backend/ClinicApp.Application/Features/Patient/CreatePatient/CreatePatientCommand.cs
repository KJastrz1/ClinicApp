using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.Features.Patient.CreatePatient;

public sealed record CreatePatientCommand(
    string Email,
    string FirstName,
    string LastName,
    string SocialSecurityNumber,
    DateTime DateOfBirth) : ICommand<Guid>;
