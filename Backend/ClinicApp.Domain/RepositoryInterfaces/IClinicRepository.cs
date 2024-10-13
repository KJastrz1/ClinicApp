using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Clinics.ValueObjects;

namespace ClinicApp.Domain.RepositoryInterfaces;

public interface IClinicRepository
{
    Task<Clinic?> GetByIdAsync(
        ClinicId id,
        CancellationToken cancellationToken);
  
    void Add(Clinic clinic);

    void Update(Clinic clinic);

    void Remove(Clinic clinic);
}
