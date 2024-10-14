using ClinicApp.Domain.Models.Doctors;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Infrastructure.Database.Contexts;
using ClinicApp.Application.ReadRepositories;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Contracts.Doctor.Requests;

namespace ClinicApp.Infrastructure.Database.Repositories.Read;

public class DoctorReadRepository : IDoctorReadRepository
{
    private readonly ReadDbContext _context;

    public DoctorReadRepository(ReadDbContext context)
    {
        _context = context;
    }

    public async Task<Doctor?> GetByIdAsync(DoctorId doctorId, CancellationToken cancellationToken)
    {
        return await _context.Doctors
            .FirstOrDefaultAsync(d => d.Id.Equals(doctorId), cancellationToken);
    }

    public async Task<PagedItems<Doctor>> GetByFilterAsync(DoctorFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        IQueryable<Doctor> query = _context.Doctors;

        if (!string.IsNullOrWhiteSpace(filter.FirstName))
        {
            query = query.Where(d => d.FirstName.Value.Contains(filter.FirstName));
        }

        if (!string.IsNullOrWhiteSpace(filter.LastName))
        {
            query = query.Where(d => d.LastName.Value.Contains(filter.LastName));
        }

        if (!string.IsNullOrWhiteSpace(filter.Specialty))
        {
            query = query.Where(d => d.SpecialtiesString.Contains(filter.Specialty));
        }

        int totalCount = await query.CountAsync(cancellationToken);

        List<Doctor> doctors = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedItems<Doctor>
        {
            Items = doctors,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
    }
}
