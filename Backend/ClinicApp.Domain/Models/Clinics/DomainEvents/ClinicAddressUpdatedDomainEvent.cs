using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Clinics.DomainEvents;

public record ClinicAddressUpdatedDomainEvent(Guid ClinicId, string NewAddress) : DomainEvent(Guid.NewGuid());
