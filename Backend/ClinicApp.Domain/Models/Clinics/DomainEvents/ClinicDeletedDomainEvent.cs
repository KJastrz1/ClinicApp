using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Clinics.DomainEvents;

public sealed record ClinicDeletedDomainEvent(Guid ClinicId) : DomainEvent(Guid.NewGuid());
