using ClinicApp.Domain.Models.Appointments;
using ClinicApp.Domain.Models.Appointments.ValueObjects;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Domain.Models.Users;
using ClinicApp.Domain.Models.Users.ValueObjects;
using ClinicApp.Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

public class AppointmentConfiguration : IWriteEntityConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable(TableNames.Appointments);

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasConversion(
                id => id.Value,
                value => AppointmentId.Create(value).Value)
            .IsRequired();

        builder.Property(a => a.DoctorId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value).Value)
            .IsRequired();

        builder.Property(a => a.PatientId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value).Value)
            .IsRequired();

        builder.Property(a => a.ClinicId)
            .HasConversion(
                id => id.Value,
                value => ClinicId.Create(value).Value)
            .IsRequired();

        builder.Property(a => a.StartDateTime)
            .HasConversion(
                startDateTime => startDateTime.Value,
                value => AppointmentStartDateTime.Create(value).Value)
            .IsRequired();

        builder.Property(a => a.DurationMinutes)
            .HasConversion(
                duration => duration.Value,
                value => AppointmentDurationMinutes.Create(value).Value)
            .IsRequired();

        builder.Property(a => a.Notes)
            .HasConversion(
                notes => notes != null ? notes.Value : null,
                value => value == null ? null : AppointmentNotes.Create(value).Value)
            .IsRequired(false);

        builder.Property(a => a.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(a => a.IsReminderSent)
            .IsRequired();

        builder.Property(a => a.CreatedOnUtc)
            .IsRequired();

        builder.Property(a => a.ModifiedOnUtc);
     
        builder.HasOne(a => a.Doctor)
            .WithMany()
            .HasForeignKey(a => a.DoctorId)
            .IsRequired();

        builder.HasOne(a => a.Patient)
            .WithMany()
            .HasForeignKey(a => a.PatientId)
            .IsRequired();

        builder.HasOne(a => a.Clinic)
            .WithMany()
            .HasForeignKey(a => a.ClinicId)
            .IsRequired();
    }
}
