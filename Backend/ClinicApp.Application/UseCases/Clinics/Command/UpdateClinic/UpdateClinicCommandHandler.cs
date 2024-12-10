using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.RepositoryInterfaces.Write;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.UseCases.Clinics.Command.UpdateClinic;

internal sealed class UpdateClinicCommandHandler : ICommandHandler<UpdateClinicCommand>
{
    private readonly IClinicRepository _clinicRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClinicCommandHandler(
        IClinicRepository clinicRepository,
        IUnitOfWork unitOfWork)
    {
        _clinicRepository = clinicRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateClinicCommand request, CancellationToken cancellationToken)
    {
        ClinicId clinicId = ClinicId.Create(request.ClinicId).Value;
        Clinic? clinic = await _clinicRepository.GetByIdAsync(clinicId, cancellationToken);
        if (clinic == null)
        {
            return Result.Failure(ClinicErrors.NotFound(clinicId));
        }

        if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            Result<PhoneNumber> phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
            clinic.UpdatePhoneNumber(phoneNumberResult.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Address))
        {
            Result<Address> addressResult = Address.Create(request.Address);
            clinic.UpdateAddress(addressResult.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.City))
        {
            Result<City> cityResult = City.Create(request.City);
            clinic.UpdateCity(cityResult.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.ZipCode))
        {
            Result<ZipCode> zipCodeResult = ZipCode.Create(request.ZipCode);
            clinic.UpdateZipCode(zipCodeResult.Value);
        }

        _clinicRepository.Update(clinic);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
