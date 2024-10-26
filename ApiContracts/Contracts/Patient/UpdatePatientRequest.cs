namespace Shared.Contracts.Patient;

public sealed record UpdatePatientRequest
(
    string? FirstName,
    string? LastName,
    string? SocialSecurityNumber,
    DateOnly? DateOfBirth
);
