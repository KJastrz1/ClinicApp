using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Patients.DomainEvents;

public sealed record PatientDeletedDomainEvent(Guid PatientId) : DomainEvent(Guid.NewGuid());
