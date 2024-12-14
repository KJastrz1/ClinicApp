namespace ClinicApp.Application.Abstractions.Authentication;

public interface IUserRoleService
{
    Task<bool> AssignRoleToUserAsync(Guid userId, string role, CancellationToken cancellationToken);
}
