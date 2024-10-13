using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Clinics.DomainEvents;

public record ClinicZipCodeUpdatedDomainEvent(Guid ClinicId, string NewZipCode) : DomainEvent(Guid.NewGuid());
