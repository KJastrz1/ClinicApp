using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Patients.ValueObjects;

namespace ClinicApp.Application.RepositoryInterfaces.Write;

public interface IPatientRepository
{
    Task<Patient?> GetByIdAsync(
        PatientId id,
        CancellationToken cancellationToken);
  
    void Add(Patient patient);

    void Update(Patient patient);

    void Remove(Patient patient);
}
