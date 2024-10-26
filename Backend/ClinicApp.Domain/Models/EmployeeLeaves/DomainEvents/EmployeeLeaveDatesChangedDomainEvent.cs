using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.EmployeeLeaves.DomainEvents;

public record EmployeeLeaveDatesChangedDomainEvent(Guid EmployeeLeaveId, DateTime NewStartDate, DateTime NewEndDate)
    : DomainEvent(Guid.NewGuid());
