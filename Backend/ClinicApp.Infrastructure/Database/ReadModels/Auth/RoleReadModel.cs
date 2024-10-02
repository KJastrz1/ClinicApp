using Shared.Contracts.Role.Responses;

namespace ClinicApp.Infrastructure.Database.ReadModels.Auth;

internal class RoleReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<PermissionReadModel> Permissions { get; set; }

    public RoleResponse ToResponse()
    {
        return new RoleResponse(
            Id,
            Name,
            Permissions?.Select(p => p.ToResponse()).ToList() ?? new List<PermissionResponse>()
        );
    }
}
