using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Shared.Contracts;

using System.Collections.Generic;
using System.Linq;
using ClinicApp.Application.RepositoryInterfaces.Read;
using ClinicApp.Infrastructure.Database.ReadModels;
using Shared.Contracts.Patient;
using Shared.Contracts.Patient.Requests;
using Shared.Contracts.Patient.Responses;
using Shared.Contracts.Shared;

namespace ClinicApp.Infrastructure.Database.Repositories.Read;

public class PatientReadRepository : IPatientReadRepository
{
    private readonly ReadDbContext _context;

    public PatientReadRepository(ReadDbContext context)
    {
        _context = context;
    }

    public async Task<PatientResponse?> GetByIdAsync(PatientId patientId, CancellationToken cancellationToken)
    {
        PatientReadModel? patientReadModel = await _context.Patients
            .FirstOrDefaultAsync(p => p.Id.Equals(patientId), cancellationToken);

        return patientReadModel?.MapToResponse();
    }

    public async Task<PagedItems<PatientResponse>> GetByFilterAsync(PatientFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        IQueryable<PatientReadModel> query = _context.Patients;

        if (!string.IsNullOrWhiteSpace(filter.FirstName))
        {
            query = query.Where(p => p.FirstName.Contains(filter.FirstName));
        }

        if (!string.IsNullOrWhiteSpace(filter.LastName))
        {
            query = query.Where(p => p.LastName.Contains(filter.LastName));
        }

        if (!string.IsNullOrWhiteSpace(filter.SocialSecurityNumber))
        {
            query = query.Where(p => p.SocialSecurityNumber.Contains(filter.SocialSecurityNumber));
        }

        if (filter.DateOfBirthStart.HasValue)
        {
            query = query.Where(p => p.DateOfBirth >= filter.DateOfBirthStart.Value);
        }

        if (filter.DateOfBirthEnd.HasValue)
        {
            query = query.Where(p => p.DateOfBirth <= filter.DateOfBirthEnd.Value);
        }

        int totalCount = await query.CountAsync(cancellationToken);

        List<PatientResponse> patients = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(p => p.MapToResponse())
            .ToListAsync(cancellationToken);

        return new PagedItems<PatientResponse>
        {
            Items = patients,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
    }
}
