using Shared.Contracts.Shared;

namespace Shared.Contracts.EmployeeLeave.Requests;

public record EmployeeLeaveFilter : AuditableFilter
{
    public Guid? EmployeeId { get; init; }
    public string? EmployeeLastName { get; init; }
    public DateRange? StartOfLeaveInRange { get; init; }
}
