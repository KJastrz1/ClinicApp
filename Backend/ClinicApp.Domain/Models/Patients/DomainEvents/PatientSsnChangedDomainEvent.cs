using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Patients.DomainEvents;

public record PatientSsnChangedDomainEvent(Guid PatientId, string NewSsn) : DomainEvent(Guid.NewGuid());
