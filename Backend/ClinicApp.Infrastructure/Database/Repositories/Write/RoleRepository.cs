using ClinicApp.Domain.Models.Roles;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.Repositories.Write;

public class RoleRepository : IRoleRepository
{
    private readonly WriteDbContext _context;

    public RoleRepository(WriteDbContext context)
    {
        _context = context;
    }
    public async Task<Role?> GetByIdAsync(RoleId id, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public void Add(Role role)
    {
        _context.Roles.Add(role);
    }

    public void Update(Role role)
    {
        _context.Roles.Update(role);
    }

    public void Remove(Role role)
    {
        _context.Roles.Remove(role);
    }
}
