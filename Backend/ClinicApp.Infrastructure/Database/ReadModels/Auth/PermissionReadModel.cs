using Shared.Contracts.Role.Responses;

namespace ClinicApp.Infrastructure.Database.ReadModels.Auth;

internal class PermissionReadModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    internal PermissionResponse ToResponse()
    {
        return new PermissionResponse(Id, Name);
    }
}
