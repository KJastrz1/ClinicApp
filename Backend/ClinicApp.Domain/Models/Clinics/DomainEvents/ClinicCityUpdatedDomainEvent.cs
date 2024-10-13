using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Clinics.DomainEvents;

public record ClinicCityUpdatedDomainEvent(Guid ClinicId, string NewCity) : DomainEvent(Guid.NewGuid());
