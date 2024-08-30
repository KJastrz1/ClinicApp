using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.User;
using ClinicApp.Domain.Models.User.ValueObjects;
using ClinicApp.Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations;

public class UserBaseConfiguration : IEntityTypeConfiguration<UserBase>
{
    public void Configure(EntityTypeBuilder<UserBase> builder)
    {
        builder.ToTable(TableNames.Users);
        
        builder.HasDiscriminator<UserType>("UserType")
            .HasValue<PatientEntity>(UserType.Patient);
        
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(
                id => id.Value,
                value => new UserId(value));

        builder.Property(u => u.Email)
            .HasConversion(
                email => email.Value,
                value => Email.Create(value).Value)
            .IsRequired();

        builder.Property(u => u.FirstName)
            .HasConversion(
                firstName => firstName.Value,
                value => FirstName.Create(value).Value)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasConversion(
                lastName => lastName.Value,
                value => LastName.Create(value).Value)
            .IsRequired();

        builder.Property(u => u.IsActivated)
            .IsRequired();

        builder.Property(u => u.CreatedOnUtc)
            .IsRequired();

        builder.Property(u => u.ModifiedOnUtc);
    }
}
