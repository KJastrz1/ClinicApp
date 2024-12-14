using ClinicApp.Infrastructure;
using ClinicApp.Infrastructure.Database.Contexts;
using ClinicApp.Infrastructure.Database.DataSeeders;
using ClinicApp.Infrastructure.Database.Interceptors;
using Microsoft.EntityFrameworkCore;
using Scrutor;

namespace ClinicApp.App.Configuration;

public class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database") ??
                                  throw new InvalidOperationException("Connection string 'Database' not found.");

        services.AddSingleton(connectionString);

        services.AddDbContext<WriteDbContext>(
            optionsBuilder => optionsBuilder.UseNpgsql(connectionString));

        services.AddDbContext<ReadDbContext>(
            optionsBuilder => optionsBuilder.UseNpgsql(connectionString));

        services
            .Scan(
                selector => selector
                    .FromAssemblies(
                        AssemblyReference.Assembly)
                    .AddClasses(false)
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsMatchingInterface()
                    .WithScopedLifetime());

        services.AddScoped<IDataSeeder, RoleSeeder>();
        
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
    }
}
