namespace Shared.Contracts.Patient.Responses;

public sealed record PatientResponse(
    Guid Id,  
    string FirstName,
    string LastName,
    string SocialSecurityNumber,
    DateTime DateOfBirth
);

