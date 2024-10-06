using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using FluentValidation;

namespace ClinicApp.Application.Actions.Roles.Command.CreateRole;

internal class UpdateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(RoleErrors.RoleNameErrors.Empty.Message)
            .MaximumLength(RoleName.MaxLength)
            .WithMessage(RoleErrors.RoleNameErrors.TooLong.Message);

        RuleForEach(x => x.PermissionsIds)
            .GreaterThan(0)
            .WithMessage(PermissionErrors.InvalidId.Message);
    }
}
