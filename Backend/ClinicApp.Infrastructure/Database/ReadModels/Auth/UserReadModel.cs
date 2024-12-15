using Microsoft.AspNetCore.Identity;
using Shared.Contracts.Auth.Responses;

namespace ClinicApp.Infrastructure.Database.ReadModels.Auth;

internal class UserReadModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool IsActivated { get; set; }
    public int AccessFailedCount { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    internal UserResponse MapToResponse()
    {
        return new UserResponse(
            Id: Id,
            UserName: UserName,
            Email: Email,
            IsActivated: IsActivated,
            EmailConfirmed: EmailConfirmed,
            PhoneNumber: PhoneNumber,
            PhoneNumberConfirmed: PhoneNumberConfirmed,
            CreatedOnUtc: CreatedOnUtc,
            ModifiedOnUtc: ModifiedOnUtc
        );
    }
}
