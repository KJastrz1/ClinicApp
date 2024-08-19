using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Patients.DomainEvents;

public record PatientDateOfBirthChangedDomainEvent(PatientId PatientId, DateOfBirth NewDateOfBirth) : DomainEvent(Guid.NewGuid());
