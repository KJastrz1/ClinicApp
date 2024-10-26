using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.EmployeeLeaves.DomainEvents;

public record EmployeeLeaveReasonChangedDomainEvent(Guid EmployeeLeaveId, string NewReason)
    : DomainEvent(Guid.NewGuid());
