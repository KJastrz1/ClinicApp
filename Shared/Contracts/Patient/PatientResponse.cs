namespace Shared.Contracts.Patient;

public sealed record PatientResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string SocialSecurityNumber,
    DateTime DateOfBirth
);
