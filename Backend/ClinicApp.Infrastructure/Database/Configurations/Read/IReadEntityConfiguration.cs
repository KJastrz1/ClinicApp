using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.Configurations.Read;

public interface IReadEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class
{
}
