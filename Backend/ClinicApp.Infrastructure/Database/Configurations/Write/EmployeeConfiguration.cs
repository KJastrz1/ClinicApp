using ClinicApp.Domain.Models.Employees;
using ClinicApp.Domain.Models.Users;
using ClinicApp.Domain.Models.Users.ValueObjects;
using ClinicApp.Infrastructure.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

public class EmployeeConfiguration : IWriteEntityConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasBaseType<User>();
   
        builder.HasKey(e => e.Id);
      
        builder.HasMany(e => e.Leaves)
            .WithOne()
            .HasForeignKey(el => el.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade); 
    
        builder.Ignore(e => e.Leaves);
    
    }
}
