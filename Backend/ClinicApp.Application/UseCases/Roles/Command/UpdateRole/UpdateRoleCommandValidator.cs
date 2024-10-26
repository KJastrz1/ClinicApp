using ClinicApp.Application.UseCases.Roles.Command.CreateRole;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using FluentValidation;

namespace ClinicApp.Application.UseCases.Roles.Command.UpdateRole;

internal class UpdateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(RoleErrors.RoleNameErrors.Empty.Message)
            .MaximumLength(RoleName.MaxLength)
            .WithMessage(RoleErrors.RoleNameErrors.TooLong.Message);
    }
}
