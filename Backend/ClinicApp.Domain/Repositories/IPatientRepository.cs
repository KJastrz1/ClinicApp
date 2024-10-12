using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Patients.ValueObjects;

namespace ClinicApp.Domain.Repositories;

public interface IPatientRepository
{
    Task<Patient?> GetByIdAsync(
        PatientId id,
        CancellationToken cancellationToken);
  
    void Add(Patient patient);

    void Update(Patient patient);

    void Remove(Patient patient);
}
