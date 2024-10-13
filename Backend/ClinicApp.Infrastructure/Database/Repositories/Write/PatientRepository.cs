using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.Repositories.Write;

public class PatientRepository : IPatientRepository
{
    private readonly WriteDbContext _writeContext;

    public PatientRepository(WriteDbContext writeContext)
    {
        _writeContext = writeContext;
    }

    public async Task<Patient?> GetByIdAsync(PatientId id, CancellationToken cancellationToken)
    {
        return await _writeContext.Patients
            .FirstOrDefaultAsync(p => p.Id.Value == id.Value, cancellationToken);
    }

    public void Add(Patient patient)
    {
        _writeContext.Patients.Add(patient);
    }

    public void Update(Patient patient)
    {
        _writeContext.Patients.Update(patient);
    }

    public void Remove(Patient patient)
    {
        _writeContext.Patients.Remove(patient);
    }
}
