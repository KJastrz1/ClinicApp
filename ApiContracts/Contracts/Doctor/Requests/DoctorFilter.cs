using Shared.Contracts.Shared;

namespace Shared.Contracts.Doctor.Requests;

public record DoctorFilter : AuditableFilter
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Specialty { get; init; }
}
