using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Shared.Contracts;
using Shared.Contracts.Clinic.Responses;
using System.Collections.Generic;
using System.Linq;
using ClinicApp.Application.RepositoryInterfaces.Read;
using ClinicApp.Infrastructure.Database.ReadModels;
using Shared.Contracts.Clinic.Requests;
using Shared.Contracts.Shared;

namespace ClinicApp.Infrastructure.Database.Repositories.Read;

public class ClinicReadRepository : IClinicReadRepository
{
    private readonly ReadDbContext _context;

    public ClinicReadRepository(ReadDbContext context)
    {
        _context = context;
    }

    public async Task<ClinicResponse?> GetByIdAsync(ClinicId clinicId, CancellationToken cancellationToken)
    {
        ClinicReadModel? clinicReadModel = await _context.Clinics
            .FirstOrDefaultAsync(c => c.Id.Equals(clinicId), cancellationToken);

        return clinicReadModel?.MapToResponse();
    }

    public async Task<PagedItems<ClinicResponse>> GetByFilterAsync(ClinicFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        IQueryable<ClinicReadModel> query = _context.Clinics;

        if (!string.IsNullOrWhiteSpace(filter.City))
        {
            query = query.Where(c => c.City.Contains(filter.City));
        }

        if (!string.IsNullOrWhiteSpace(filter.ZipCode))
        {
            query = query.Where(c => c.ZipCode.Contains(filter.ZipCode));
        }

        if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
        {
            query = query.Where(c => c.PhoneNumber.Contains(filter.PhoneNumber));
        }

        if (!string.IsNullOrWhiteSpace(filter.Address))
        {
            query = query.Where(c => c.Address.Contains(filter.Address));
        }

        int totalCount = await query.CountAsync(cancellationToken);

        List<ClinicResponse> clinics = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(c => c.MapToResponse())
            .ToListAsync(cancellationToken);

        return new PagedItems<ClinicResponse>
        {
            Items = clinics,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
    }
}
