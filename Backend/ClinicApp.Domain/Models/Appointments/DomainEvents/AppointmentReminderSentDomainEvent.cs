using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Appointments.DomainEvents;

public sealed record AppointmentReminderSentDomainEvent(Guid AppointmentId) : DomainEvent(Guid.NewGuid());
