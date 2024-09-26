using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.Configurations.Write;

public interface IWriteEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class
{
}
