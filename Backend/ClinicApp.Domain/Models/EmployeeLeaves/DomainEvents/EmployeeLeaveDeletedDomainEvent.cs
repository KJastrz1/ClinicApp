using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.EmployeeLeaves.DomainEvents;

public record EmployeeLeaveDeletedDomainEvent(Guid EmployeeLeaveId) : DomainEvent(Guid.NewGuid());
