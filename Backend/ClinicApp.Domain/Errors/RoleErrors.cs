using ClinicApp.Domain.Models.Roles.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Errors;

public static class RoleErrors
{
    public static Error NotFound(RoleId id) => new(
        "Roles.NotFound",
        $"The role with Id {id.Value} was not found");

    public static Error EmptyId = new(
        "Roles.EmptyId",
        "Role's ID is empty");

    public static readonly Error NameAlreadyExists = new(
        "Role.NameAlreadyExists",
        "Role with the specified name already exists.");

    public static class RoleNameErrors
    {
        public static readonly Error Empty = new(
            "RoleName.Empty",
            "Role name is empty.");

        public static readonly Error TooLong = new(
            "RoleName.TooLong",
            $"Role name must not exceed {RoleName.MaxLength} characters.");
    }
}
