namespace ClinicApp.Infrastructure.Database.ReadModels.Auth;

internal class RoleReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<PermissionReadModel> Permissions { get; set; }
}
