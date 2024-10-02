using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Domain.Models.Roles.ValueObjects;

namespace ClinicApp.Application.Abstractions.Authentication;

public interface IUserContext
{
    AccountId AccountId { get; }
    Email Email { get; }
    IEnumerable<RoleName> RoleNames { get; }
    HashSet<string> Permissions { get; }
}
