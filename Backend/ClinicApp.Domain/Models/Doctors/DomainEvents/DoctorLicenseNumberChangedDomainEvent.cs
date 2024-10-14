using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Doctors.DomainEvents;

public record DoctorLicenseNumberChangedDomainEvent(Guid PatientId, string NewLicenseNumber) : DomainEvent(Guid.NewGuid());
