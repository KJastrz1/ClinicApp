using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.Repositories.Write;

public class ClinicRepository : IClinicRepository
{
    private readonly WriteDbContext _writeContext;

    public ClinicRepository(WriteDbContext writeContext)
    {
        _writeContext = writeContext;
    }

    public async Task<Clinic?> GetByIdAsync(ClinicId id, CancellationToken cancellationToken)
    {
        return await _writeContext.Clinics
            .FirstOrDefaultAsync(c => c.Id.Value == id.Value, cancellationToken);
    }

    public void Add(Clinic clinic)
    {
        _writeContext.Clinics.Add(clinic);
    }

    public void Update(Clinic clinic)
    {
        _writeContext.Clinics.Update(clinic);
    }

    public void Remove(Clinic clinic)
    {
        _writeContext.Clinics.Remove(clinic);
    }
}
