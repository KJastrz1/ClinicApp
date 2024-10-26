using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.EmployeeLeaves.DomainEvents;

public record EmployeeLeaveStatusChangedDomainEvent(Guid EmployeeLeaveId, LeaveStatusEnum NewStatus)
    : DomainEvent(Guid.NewGuid());
