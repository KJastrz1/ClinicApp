using ClinicApp.Domain.Enums;

namespace Shared.Contracts.EmployeeLeave.Requests;

public record ChangeLeaveStatusRequest(LeaveStatusEnum NewStatus);
