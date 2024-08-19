using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Models.User.ValueObjects;

namespace ClinicApp.Domain.Repositories;

public interface IPatientRepository
{
    Task<PatientEntity?> GetByIdAsync(PatientId id, CancellationToken cancellationToken);

    Task<IReadOnlyList<PatientEntity>> GetAllAsync(CancellationToken cancellationToken);
    void Add(PatientEntity patient);

    void Update(PatientEntity patient);

    void Remove(PatientEntity patient);

    Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken);
}
