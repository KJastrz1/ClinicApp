using Shared.Contracts.Clinic.Responses;

namespace ClinicApp.Infrastructure.Database.ReadModels;

internal class ClinicReadModel
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    internal ClinicResponse MapToResponse()
    {
        return new ClinicResponse(
            Id: Id,
            PhoneNumber: PhoneNumber,
            Address: Address,
            City: City,
            ZipCode: ZipCode
        );
    }
}
