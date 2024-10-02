namespace Shared.Contracts.Patient;

public sealed record RegisterPatientRequest
(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string SocialSecurityNumber,
    DateOnly DateOfBirth
);
