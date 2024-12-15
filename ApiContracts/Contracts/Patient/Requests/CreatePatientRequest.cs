namespace Shared.Contracts.Patient.Requests;

public sealed record CreatePatientRequest
(
    string FirstName,
    string LastName,
    string SocialSecurityNumber,
    DateOnly DateOfBirth
);
    

