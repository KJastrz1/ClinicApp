using Shared.Contracts.Shared;

namespace Shared.Contracts.Patient.Requests;

public record PatientFilter : AuditableFilter
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public bool? IsActivated { get; init; }
    public string? SocialSecurityNumber { get; init; }
    public DateTime? DateOfBirthStart { get; init; }
    public DateTime? DateOfBirthEnd { get; init; }
}
