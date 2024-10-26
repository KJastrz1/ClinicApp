using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Appointments.DomainEvents;

public record AppointmentNotesUpdatedDomainEvent(Guid AppointmentId, string Notes) : DomainEvent(Guid.NewGuid());
