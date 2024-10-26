using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.EmployeeLeaves.DomainEvents;

public record EmployeeLeaveCreatedDomainEvent(
    Guid EmployeeLeaveId,
    Guid EmployeeId,
    DateTime StartDate,
    DateTime EndDate) : DomainEvent(Guid.NewGuid());
