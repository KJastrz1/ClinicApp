using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Models.Roles.ValueObjects;

namespace ClinicApp.Domain.Models.Roles;

public class AccountRole
{
    public AccountId AccountId { get; set; }

    public RoleId RoleId { get; set; }
}
