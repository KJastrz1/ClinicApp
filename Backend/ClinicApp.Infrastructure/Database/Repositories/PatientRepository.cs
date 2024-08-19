using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Models.User.ValueObjects;
using ClinicApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _context;

    public PatientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PatientEntity?> GetByIdAsync(PatientId id, CancellationToken cancellationToken)
    {
        return await _context.Patients
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PatientEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Patients
            .ToListAsync(cancellationToken);
    }

    public void Add(PatientEntity patient)
    {
        _context.Patients.Add(patient);
    }

    public void Update(PatientEntity patient)
    {
        _context.Patients.Update(patient);
    }

    public void Remove(PatientEntity patient)
    {
        _context.Patients.Remove(patient);
    }

    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken)
    {
        return !await _context.Patients
            .AnyAsync(p => p.Email == email, cancellationToken);
    }
}
