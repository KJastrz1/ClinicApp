using ClinicApp.Domain.Models.Employees;
using ClinicApp.Domain.Models.UserProfiles;
using ClinicApp.Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

public class EmployeeConfiguration : IWriteEntityConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable(TableNames.Employees);
        
        builder.HasBaseType<UserProfile>();
      
        builder.HasMany(e => e.Leaves)
            .WithOne(el=> el.Employee)
            .HasForeignKey(el => el.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
