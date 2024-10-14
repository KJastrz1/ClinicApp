using ClinicApp.Domain.Models.Doctors;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.Models.Users.ValueObjects;
using ClinicApp.Domain.Shared;
using ClinicApp.Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

public class DoctorConfiguration : IWriteEntityConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable(TableNames.Doctors);
        
        builder.Property(d => d.MedicalLicenseNumber)
            .HasConversion(
                licenseNumber => licenseNumber.Value,
                value => MedicalLicenseNumber.Create(value).Value)
            .IsRequired();

        builder.Property(d => d.Bio)
            .HasConversion(
                bio => bio != null ? bio.Value : null,
                value => value == null ? null : Bio.Create(value).Value)
            .IsRequired(false);

        builder.Property(d => d.AcademicTitle)
            .HasConversion(
                title => title != null ? title.Value : null,
                value => value == null ? null : AcademicTitle.Create(value).Value)
            .IsRequired(false);

        builder.Property(d => d.SpecialtiesString)
            .IsRequired();

        builder.Property(d => d.CreatedOnUtc)
            .IsRequired();

        builder.Property(d => d.ModifiedOnUtc);

        builder.HasOne(d => d.Account)
            .WithOne()
            .HasForeignKey<Doctor>(d => d.AccountId)
            .IsRequired(false);

        builder.HasOne(d => d.Clinic)
            .WithMany()
            .HasForeignKey(d => d.ClinicId)
            .IsRequired(false);
    }

    private List<Specialty> ConvertToSpecialties(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new List<Specialty>();
        }

        return value.Split(',')
            .Select(s => Specialty.Create(s).Value)
            .ToList();
    }
}
