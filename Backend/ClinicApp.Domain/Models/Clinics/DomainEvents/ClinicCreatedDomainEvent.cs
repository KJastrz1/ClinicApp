using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Clinics.DomainEvents;

public sealed record ClinicCreatedDomainEvent(Guid ClinicId) : DomainEvent(Guid.NewGuid());
