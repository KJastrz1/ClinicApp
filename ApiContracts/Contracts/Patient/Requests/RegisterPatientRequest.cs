namespace Shared.Contracts.Patient.Requests;

public sealed record RegisterPatientRequest
(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string SocialSecurityNumber,
    DateOnly DateOfBirth
);
