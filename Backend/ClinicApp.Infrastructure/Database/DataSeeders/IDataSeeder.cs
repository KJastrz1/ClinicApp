using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.DataSeeders;

public interface IDataSeeder
{
    void Seed(ModelBuilder modelBuilder);
}
