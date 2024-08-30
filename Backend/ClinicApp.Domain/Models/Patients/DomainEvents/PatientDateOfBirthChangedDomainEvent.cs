using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Patients.DomainEvents;

public record PatientDateOfBirthChangedDomainEvent(Guid PatientId, DateOfBirth NewDateOfBirth) : DomainEvent(Guid.NewGuid());
