using ClinicApp.Application.RepositoryInterfaces;
using ClinicApp.Application.RepositoryInterfaces.Write;
using ClinicApp.Domain.Models.Doctors;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.Repositories.Write;

public class DoctorRepository : IDoctorRepository
{
    private readonly WriteDbContext _writeContext;

    public DoctorRepository(WriteDbContext writeContext)
    {
        _writeContext = writeContext;
    }

    public async Task<Doctor?> GetByIdAsync(DoctorId id, CancellationToken cancellationToken)
    {
        return await _writeContext.Doctors
            .Include(d => d.Specialties)
            .FirstOrDefaultAsync(d => d.Id.Value == id.Value, cancellationToken);
    }

    public void Add(Doctor doctor)
    {
        _writeContext.Doctors.Add(doctor);
    }

    public void Update(Doctor doctor)
    {
        _writeContext.Doctors.Update(doctor);
    }

    public void Remove(Doctor doctor)
    {
        _writeContext.Doctors.Remove(doctor);
    }
}
