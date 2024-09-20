using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
      
        // builder.Property(u => u.Id)
        //     .HasConversion(
        //         id => id.Value,
        //         value => PatientId.Create(value).Value);
        
        builder.Property(p => p.SocialSecurityNumber)
            .HasConversion(ssn => ssn.Value, v => SocialSecurityNumber.Create(v).Value).
            IsRequired();

        builder.Property(p => p.DateOfBirth)
            .HasConversion(dob => dob.Value, v => DateOfBirth.Create(v).Value).IsRequired();
    }
}
