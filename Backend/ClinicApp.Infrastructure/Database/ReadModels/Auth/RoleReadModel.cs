namespace ClinicApp.Infrastructure.Database.ReadModels.Auth;

internal class RoleReadModel
{
    public string Name { get; set; }
    public List<PermissionReadModel> Permissions { get; set; }
}
