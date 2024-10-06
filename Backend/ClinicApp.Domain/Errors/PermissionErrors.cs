using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Errors;

public static class PermissionErrors
{
    public static Error NotFound(int id) => new(
        "Permissions.NotFound",
        $"The permission with Id {id} was not found");

    public static Error InvalidId = new(
        "Permission.InvalidId",
        "The permission ID must be greater than 0.");

    public static Error EmptyId = new(
        "Permission.EmptyId",
        "Permission's ID is empty");
}
