using ClinicApp.Domain.Models.Roles;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using ClinicApp.Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

internal sealed class RoleConfiguration : IWriteEntityConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.Roles);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => RoleId.Create(value).Value);

        builder.Property(x => x.Name).HasConversion(
                x => x.Value,
                x => RoleName.Create(x).Value)
            .IsRequired()
            .HasMaxLength(RoleName.MaxLength);

        builder.HasIndex(x => x.Name)
            .IsUnique();
        
        builder
            .HasMany(r => r.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();
    }
}
