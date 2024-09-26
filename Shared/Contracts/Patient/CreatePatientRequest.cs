namespace Shared.Contracts.Patient;

public sealed record CreatePatientRequest
(
    string Email,
    string FirstName,
    string LastName,
    string SocialSecurityNumber,
    DateOnly DateOfBirth
);
    

