using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Appointments.DomainEvents;

public record AppointmentDeletedDomainEvent(Guid AppointmentId) : DomainEvent(Guid.NewGuid());
