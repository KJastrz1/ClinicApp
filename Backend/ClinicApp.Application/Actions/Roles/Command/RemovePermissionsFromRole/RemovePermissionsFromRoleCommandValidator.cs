using ClinicApp.Domain.Errors;
using FluentValidation;

namespace ClinicApp.Application.Actions.Roles.Command.RemovePermissionsFromRole;

internal class RemovePermissionsFromRoleCommandValidator : AbstractValidator<RemovePermissionsFromRoleCommand>
{
    public RemovePermissionsFromRoleCommandValidator()
    {
        RuleForEach(x => x.PermissionsIds)
            .GreaterThan(0)
            .WithMessage(PermissionErrors.InvalidId.Message);
    }
}
