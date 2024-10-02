using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.DataSeeders;

public interface IEFDataSeeder
{
    void Seed(ModelBuilder modelBuilder);
}
