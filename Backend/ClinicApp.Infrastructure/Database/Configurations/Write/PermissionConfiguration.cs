using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableNames.Permissions);

        builder.HasKey(p => p.Id);

        IEnumerable<Permission> permissions = Enum
            .GetValues<PermissionEnum>()
            .Select(p => new Permission
            {
                Id = (int)p,
                Name = p.ToString()
            });

        builder.HasData(permissions);
    }
}
