using ClinicApp.Infrastructure;
using ClinicApp.Infrastructure.Database;
using ClinicApp.Infrastructure.Database.Interceptors;
using Microsoft.EntityFrameworkCore;
using Scrutor;

namespace ClinicApp.App.Configuration;

public class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .Scan(
                selector => selector
                    .FromAssemblies(
                        AssemblyReference.Assembly)
                    .AddClasses(false)
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsMatchingInterface()
                    .WithScopedLifetime());

        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

        services.AddDbContext<ApplicationDbContext>(
            (optionsBuilder) =>
            {
                optionsBuilder.UseNpgsql(
                    configuration.GetConnectionString("Database"));
            });
    }
}
