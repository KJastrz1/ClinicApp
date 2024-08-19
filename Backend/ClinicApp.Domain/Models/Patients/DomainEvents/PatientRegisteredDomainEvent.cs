using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Patients.DomainEvents;

public sealed record PatientRegisteredDomainEvent(Guid PatientId) : DomainEvent(Guid.NewGuid());
