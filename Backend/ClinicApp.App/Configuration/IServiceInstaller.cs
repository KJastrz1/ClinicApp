namespace ClinicApp.App.Configuration;

public interface IServiceInstaller
{
    void Install(IServiceCollection services, IConfiguration configuration);
}
