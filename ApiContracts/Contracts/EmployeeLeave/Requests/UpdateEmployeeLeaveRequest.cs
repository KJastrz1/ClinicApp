namespace Shared.Contracts.EmployeeLeave.Requests;

public record UpdateEmployeeLeaveRequest(
    Guid EmployeeLeaveId,
    string? Reason,
    DateTime? StartDate,
    DateTime? EndDate
);
