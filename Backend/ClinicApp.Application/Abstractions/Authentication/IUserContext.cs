namespace ClinicApp.Application.Abstractions.Authentication;

public interface IUserContext
{
    string Id { get; }
    string UserName { get; }
    string Email { get; }
    string PhoneNumber { get; }
}
