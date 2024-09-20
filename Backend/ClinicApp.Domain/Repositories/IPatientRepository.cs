using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using Shared.Contracts;
using Shared.Contracts.Patient;

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
