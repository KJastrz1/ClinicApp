using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.EmployeeLeaves;
using ClinicApp.Domain.Models.EmployeeLeaves.ValueObjects;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using ClinicApp.Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

public class EmployeeLeaveConfiguration : IWriteEntityConfiguration<EmployeeLeave>
{
    public void Configure(EntityTypeBuilder<EmployeeLeave> builder)
    {
        builder.ToTable(TableNames.EmployeeLeaves);

        builder.HasKey(el => el.Id);

        builder.Property(el => el.Id)
            .HasConversion(
                id => id.Value,
                value => EmployeeLeaveId.Create(value).Value);

        builder.Property(el => el.EmployeeId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value).Value)
            .IsRequired();


        builder.Property(el => el.Reason)
            .HasConversion(
                reason => reason.Value,
                value => LeaveReason.Create(value).Value)
            .IsRequired();


        builder.Property(el => el.Status)
            .HasConversion(
                status => status.ToString(),
                value => Enum.Parse<LeaveStatusEnum>(value))
            .IsRequired();


        builder.Property(el => el.StartDate)
            .HasConversion(
                startDate => startDate.Value,
                value => LeaveStartDate.Create(value).Value)
            .IsRequired();


        builder.Property(el => el.EndDate)
            .HasConversion(
                endDate => endDate.Value,
                value => LeaveEndDate.Create(value).Value)
            .IsRequired();

        builder.Property(el => el.CreatedOnUtc)
            .IsRequired();

        builder.Property(el => el.ModifiedOnUtc);
    }
}
