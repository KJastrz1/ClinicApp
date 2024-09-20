using ClinicApp.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace ClinicApp.Infrastructure.Authentication;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(PermissionEnum permission)
        : base(policy: permission.ToString())
    {
    }
}
