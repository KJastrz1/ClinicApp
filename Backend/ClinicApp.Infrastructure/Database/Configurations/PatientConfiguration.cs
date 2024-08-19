using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<PatientEntity>
{
    public void Configure(EntityTypeBuilder<PatientEntity> builder)
    {
      
        builder.ToTable("Patients");
        
        builder.HasKey(u => u.Id);
        
        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => new PatientId(value));

        builder.Property(p => p.SocialSecurityNumber)
            .HasConversion(ssn => ssn.Value, v => SocialSecurityNumber.Create(v).Value);

        builder.Property(p => p.DateOfBirth)
            .HasConversion(dob => dob.Value, v => DateOfBirth.Create(v).Value);
    }
}
