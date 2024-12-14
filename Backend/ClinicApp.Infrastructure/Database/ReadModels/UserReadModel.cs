using ClinicApp.Domain.Enums;

namespace ClinicApp.Infrastructure.Database.ReadModels;

internal class UserReadModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserRole UserRole { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public Guid? AccountId { get; set; }
}
