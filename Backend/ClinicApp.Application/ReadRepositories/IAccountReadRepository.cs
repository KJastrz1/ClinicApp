using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.Accounts.ValueObjects;


namespace ClinicApp.Application.ReadRepositories;

public interface IAccountReadRepository
{
    Task<Account?> GetByEmailAsync(Email email, CancellationToken cancellationToken);
    
    Task<Account?> GetByEmailWithRolesAsync(Email email, CancellationToken cancellationToken);
}
