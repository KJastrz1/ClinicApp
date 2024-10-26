namespace ClinicApp.Domain.RepositoryInterfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}