namespace ClinicApp.Application.RepositoryInterfaces.Write;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
