using ClinicApp.Domain.Models.UserProfiles.ValueObjects;

namespace ClinicApp.Application.Abstractions.Authentication;

public interface IUserContext
{
    UserId Id { get; }
    string Email { get; }
    string PhoneNumber { get; }
    bool IsEmailConfirmed { get; }
    bool IsPhoneNumberConfirmed { get; }
    Task<IList<string>> GetRolesAsync();
}
