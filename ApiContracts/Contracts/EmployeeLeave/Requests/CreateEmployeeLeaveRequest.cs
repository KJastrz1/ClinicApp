using System;

namespace Shared.Contracts.EmployeeLeave.Requests;

public record CreateEmployeeLeaveRequest
{
    public Guid EmployeeId { get; init; }
    public string Reason { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}
