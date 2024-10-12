using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.Actions.Patients.Command.UpdatePatient;

public sealed record UpdatePatientCommand(
    Guid PatientId,
    string FirstName,
    string LastName,
    string SocialSecurityNumber,
    DateOnly DateOfBirth) : ICommand<Guid>;
