using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Appointments.DomainEvents;

public sealed record AppointmentStatusUpdatedDomainEvent(Guid AppointmentId, AppointmentStatusEnum NewStatus) : DomainEvent(Guid.NewGuid());
