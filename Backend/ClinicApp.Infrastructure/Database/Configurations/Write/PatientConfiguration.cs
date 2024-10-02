using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

public class PatientConfiguration : IWriteEntityConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.Property(p => p.SocialSecurityNumber)
            .HasConversion(ssn => ssn.Value, v => SocialSecurityNumber.Create(v).Value)
            .IsRequired();

        builder.Property(p => p.DateOfBirth)
            .HasConversion(dob => dob.Value, v => DateOfBirth.Create(v).Value)
            .IsRequired();
    }
}