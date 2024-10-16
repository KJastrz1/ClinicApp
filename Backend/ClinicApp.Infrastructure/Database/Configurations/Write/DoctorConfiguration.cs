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
        
        builder.OwnsMany(d => d.Specialties, specialty =>
        {
            specialty.WithOwner().HasForeignKey("DoctorId");
            
            specialty.HasKey("DoctorId", "Value");
        });
        
        builder.OwnsMany(d => d.Schedules, schedule =>
        {
            schedule.WithOwner().HasForeignKey("DoctorId");
            schedule.Property(s => s.Day).HasConversion(d => d.Value, value => ScheduleDay.Create(value).Value)
                .IsRequired();


            schedule.Property(s => s.StartTime).HasConversion(st => st.Value, value => StartTime.Create(value).Value)
                .IsRequired();

            schedule.Property(s => s.EndTime).HasConversion(et => et.Value, value => EndTime.Create(value).Value)
                .IsRequired();
            schedule.Property(s => s.VisitDuration)
                .HasConversion(vd => vd.Value, value => VisitDuration.Create(value).Value).IsRequired();
            
            schedule.HasKey(s => s.Id);

            schedule.Property(s => s.Id)
                .HasConversion(
                    id => id.Value,
                    value => DoctorScheduleId.Create(value).Value);
        });
    }
}
