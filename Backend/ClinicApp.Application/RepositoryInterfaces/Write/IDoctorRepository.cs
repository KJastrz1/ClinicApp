using ClinicApp.Domain.Models.Doctors;
using ClinicApp.Domain.Models.Doctors.ValueObjects;

namespace ClinicApp.Application.RepositoryInterfaces.Write;

public interface IDoctorRepository
{
    Task<Doctor?> GetByIdAsync(
        DoctorId id,
        CancellationToken cancellationToken);
  
    void Add(Doctor doctor);

    void Update(Doctor doctor);

    void Remove(Doctor doctor);
}
