namespace Shared.Contracts.Patient.Requests;

public sealed record UpdatePatientRequest
(
    string? FirstName,
    string? LastName,
    string? SocialSecurityNumber,
    DateOnly? DateOfBirth
);
