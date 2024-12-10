using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.RepositoryInterfaces.Write;
using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.UseCases.Clinics.Command.CreateClinic;

internal sealed class CreateClinicCommandHandler : ICommandHandler<CreateClinicCommand, Guid>
{
    private readonly IClinicRepository _clinicRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClinicCommandHandler(
        IClinicRepository clinicRepository,
        IUnitOfWork unitOfWork)
    {
        _clinicRepository = clinicRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateClinicCommand request, CancellationToken cancellationToken)
    {
        Result<PhoneNumber> phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
        Result<Address> addressResult = Address.Create(request.Address);
        Result<City> cityResult = City.Create(request.City);
        Result<ZipCode> zipCodeResult = ZipCode.Create(request.ZipCode);

        var clinic = Clinic.Create(
            ClinicId.New(),
            phoneNumberResult.Value,
            addressResult.Value,
            cityResult.Value,
            zipCodeResult.Value
        );

        _clinicRepository.Add(clinic);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return clinic.Id.Value;
    }
}
