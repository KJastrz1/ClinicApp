using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Infrastructure.Database.Contexts;
using ClinicApp.Application.ReadRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Shared.Contracts;
using Shared.Contracts.Clinic;
using Shared.Contracts.Clinic.Requests;
using Shared.Contracts.Clinic.Responses;

namespace ClinicApp.Infrastructure.Database.Repositories.Read;

public class ClinicReadRepository : IClinicReadRepository
{
    private readonly ReadDbContext _context;

    public ClinicReadRepository(ReadDbContext context)
    {
        _context = context;
    }

    public async Task<Clinic?> GetByIdAsync(ClinicId clinicId, CancellationToken cancellationToken)
    {
        return await _context.Clinics
            .FirstOrDefaultAsync(c => c.Id.Equals(clinicId), cancellationToken);
    }

    public async Task<PagedItems<Clinic>> GetByFilterAsync(ClinicFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        IQueryable<Clinic> query = _context.Clinics;

        if (!string.IsNullOrWhiteSpace(filter.City))
        {
            query = query.Where(c => c.City.Value.Contains(filter.City));
        }

        if (!string.IsNullOrWhiteSpace(filter.ZipCode))
        {
            query = query.Where(c => c.ZipCode.Value.Contains(filter.ZipCode));
        }

        if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
        {
            query = query.Where(c => c.PhoneNumber.Value.Contains(filter.PhoneNumber));
        }

        if (!string.IsNullOrWhiteSpace(filter.Address))
        {
            query = query.Where(c => c.Address.Value.Contains(filter.Address));
        }

        int totalCount = await query.CountAsync(cancellationToken);

        List<Clinic> clinics = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedItems<Clinic>
        {
            Items = clinics,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
    }
}
