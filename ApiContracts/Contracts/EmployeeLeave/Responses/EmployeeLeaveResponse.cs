using System;
using ClinicApp.Domain.Enums;
using Shared.Contracts.Employee;

namespace Shared.Contracts.EmployeeLeave.Responses;

public record EmployeeLeaveResponse(
    Guid Id,
    EmployeeResponse Employee,
    string Reason,
    LeaveStatusEnum Status,
    DateTime StartDate,
    DateTime EndDate
);
