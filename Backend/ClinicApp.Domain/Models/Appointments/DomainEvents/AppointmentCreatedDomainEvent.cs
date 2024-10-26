using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Appointments.DomainEvents;

public sealed record AppointmentCreatedDomainEvent(Guid AppointmentId) : DomainEvent(Guid.NewGuid());
