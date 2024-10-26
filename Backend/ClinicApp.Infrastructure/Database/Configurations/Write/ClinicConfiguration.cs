using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

public class ClinicConfiguration : IWriteEntityConfiguration<Clinic>
{
    public void Configure(EntityTypeBuilder<Clinic> builder)
    {
        builder.ToTable(TableNames.Clinics);
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(
                id => id.Value,
                value => ClinicId.Create(value).Value);

        builder.Property(c => c.PhoneNumber)
            .HasConversion(
                phoneNumber => phoneNumber.Value,
                value => PhoneNumber.Create(value).Value)
            .IsRequired();

        builder.Property(c => c.Address)
            .HasConversion(
                address => address.Value,
                value => Address.Create(value).Value)
            .IsRequired();

        builder.Property(c => c.City)
            .HasConversion(
                city => city.Value,
                value => City.Create(value).Value)
            .HasColumnName("City")
            .HasMaxLength(City.MaxLength)
            .IsRequired();

        builder.Property(c => c.ZipCode)
            .HasConversion(
                zipCode => zipCode.Value,
                value => ZipCode.Create(value).Value)
            .IsRequired();

        builder.Property(c => c.CreatedOnUtc)
            .IsRequired();

        builder.Property(c => c.ModifiedOnUtc);
    }
}
