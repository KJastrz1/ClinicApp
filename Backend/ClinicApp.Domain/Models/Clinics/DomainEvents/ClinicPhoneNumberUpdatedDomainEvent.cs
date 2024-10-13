using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Clinics.DomainEvents;

public record ClinicPhoneNumberUpdatedDomainEvent(Guid ClinicId, string NewPhoneNumber) : DomainEvent(Guid.NewGuid());
