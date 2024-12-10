using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Shared.Contracts;
using Shared.Contracts.Doctor.Responses;
using System.Collections.Generic;
using System.Linq;
using ClinicApp.Application.RepositoryInterfaces.Read;
using ClinicApp.Infrastructure.Database.ReadModels;
using Shared.Contracts.Doctor.Requests;
using Shared.Contracts.Shared;

namespace ClinicApp.Infrastructure.Database.Repositories.Read;

public class DoctorReadRepository : IDoctorReadRepository
{
    private readonly ReadDbContext _context;

    public DoctorReadRepository(ReadDbContext context)
    {
        _context = context;
    }

    public async Task<DoctorResponse?> GetByIdAsync(DoctorId doctorId, CancellationToken cancellationToken)
    {
        DoctorReadModel? doctorReadModel = await _context.Doctors
            .FirstOrDefaultAsync(d => d.Id.Equals(doctorId), cancellationToken);

        return doctorReadModel?.MapToResponse();
    }

    public async Task<PagedItems<DoctorResponse>> GetByFilterAsync(DoctorFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        IQueryable<DoctorReadModel> query = _context.Doctors;

        if (!string.IsNullOrWhiteSpace(filter.FirstName))
        {
            query = query.Where(d => d.FirstName.Contains(filter.FirstName));
        }

        if (!string.IsNullOrWhiteSpace(filter.LastName))
        {
            query = query.Where(d => d.LastName.Contains(filter.LastName));
        }

        if (!string.IsNullOrWhiteSpace(filter.Specialty))
        {
            query = query.Where(d => d.Specialties.Contains(filter.Specialty));
        }

        int totalCount = await query.CountAsync(cancellationToken);

        List<DoctorResponse> doctors = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(d => d.MapToResponse())
            .ToListAsync(cancellationToken);

        return new PagedItems<DoctorResponse>
        {
            Items = doctors,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
    }
}
