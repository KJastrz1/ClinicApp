using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Appointments.DomainEvents;

public record AppointmentCreatedDomainEvent(Guid Id) : IDomainEvent;
