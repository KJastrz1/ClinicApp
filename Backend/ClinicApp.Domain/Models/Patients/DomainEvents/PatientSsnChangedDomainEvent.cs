using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Patients.DomainEvents;

public record PatientSsnChangedDomainEvent(Guid PatientId, SocialSecurityNumber NewSsn) : DomainEvent(Guid.NewGuid());
