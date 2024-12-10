using System;
using System.Threading;
using System.Threading.Tasks;
using ClinicApp.Application.RepositoryInterfaces.Write;
using ClinicApp.Application.UseCases.Clinics.Command.UpdateClinic;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Clinics.DomainEvents;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;
using FluentAssertions;
using Moq;
using Xunit;

namespace ClinicApp.UnitTests.Clinics.Command;

public class UpdateClinicCommandTests
{
    private readonly Mock<IClinicRepository> _clinicRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UpdateClinicCommandHandler _handler;

    public UpdateClinicCommandTests()
    {
        _clinicRepositoryMock = new Mock<IClinicRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new UpdateClinicCommandHandler(
            _clinicRepositoryMock.Object,
            _unitOfWorkMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenClinicNotFound()
    {
        // Arrange
        var command = new UpdateClinicCommand(
            Guid.NewGuid(),
            "123456789",
            "456 Updated St",
            "New City",
            "67890"
        );

        _clinicRepositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<ClinicId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Clinic?)null);

        // Act
        Result result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ClinicErrors.NotFound(ClinicId.Create(command.ClinicId).Value));
        
        _clinicRepositoryMock.Verify(repo => repo.Update(It.IsAny<Clinic>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldUpdateClinic_WhenFieldsAreProvided()
    {
        // Arrange
        var clinicId = Guid.NewGuid();
        var clinic = Clinic.Create(
            ClinicId.Create(clinicId).Value,
            PhoneNumber.Create("56").Value, 
            Address.Create("123 Main St").Value, 
            City.Create("Some City").Value, 
            ZipCode.Create("12345").Value 
        );
        Clinic? clinicForVerification = null;

        var command = new UpdateClinicCommand(
            clinicId,
            "123456789",
            "456 Updated St",
            "New City",
            "67890"
        );

        _clinicRepositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<ClinicId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(clinic);

        _clinicRepositoryMock
            .Setup(repo => repo.Update(It.IsAny<Clinic>()))
            .Callback<Clinic>(c => clinicForVerification = c);

        // Act
        Result result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        clinicForVerification.Should().NotBeNull();
        clinicForVerification!.PhoneNumber.Value.Should().Be(command.PhoneNumber);
        clinicForVerification.Address.Value.Should().Be(command.Address);
        clinicForVerification.City.Value.Should().Be(command.City);
        clinicForVerification.ZipCode.Value.Should().Be(command.ZipCode);
        
        _clinicRepositoryMock.Verify(repo => repo.Update(It.IsAny<Clinic>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        
        IReadOnlyCollection<IDomainEvent> domainEvents = clinic.GetDomainEvents();
        domainEvents.Should().ContainSingle(e => 
            e is ClinicPhoneNumberUpdatedDomainEvent 
            && ((ClinicPhoneNumberUpdatedDomainEvent)e).ClinicId == clinicId
            && ((ClinicPhoneNumberUpdatedDomainEvent)e).NewPhoneNumber == command.PhoneNumber);
       
        domainEvents.Should().ContainSingle(e => 
            e is ClinicAddressUpdatedDomainEvent 
            && ((ClinicAddressUpdatedDomainEvent)e).ClinicId == clinicId
            && ((ClinicAddressUpdatedDomainEvent)e).NewAddress == command.Address);
      
        domainEvents.Should().ContainSingle(e => 
            e is ClinicCityUpdatedDomainEvent 
            && ((ClinicCityUpdatedDomainEvent)e).ClinicId == clinicId
            && ((ClinicCityUpdatedDomainEvent)e).NewCity == command.City);
   
        domainEvents.Should().ContainSingle(e => 
            e is ClinicZipCodeUpdatedDomainEvent 
            && ((ClinicZipCodeUpdatedDomainEvent)e).ClinicId == clinicId
            && ((ClinicZipCodeUpdatedDomainEvent)e).NewZipCode == command.ZipCode);
    }

    [Fact]
    public async Task Handle_ShouldNotUpdate_WhenNoFieldsProvided()
    {
        // Arrange
        var clinicId = Guid.NewGuid();
        var clinic = Clinic.Create(
            ClinicId.Create(clinicId).Value,
            PhoneNumber.Create("56").Value, 
            Address.Create("123 Main St").Value, 
            City.Create("Some City").Value, 
            ZipCode.Create("12345").Value 
        );
        Clinic? clinicForVerification = null;

        var command = new UpdateClinicCommand(clinicId, null, null, null, null);

        _clinicRepositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<ClinicId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(clinic);

        _clinicRepositoryMock
            .Setup(repo => repo.Update(It.IsAny<Clinic>()))
            .Callback<Clinic>(c => clinicForVerification = c);

        // Act
        Result result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        
        clinicForVerification.Should().NotBeNull();
        clinicForVerification!.PhoneNumber.Should().Be(clinic.PhoneNumber);
        clinicForVerification.Address.Should().Be(clinic.Address);
        clinicForVerification.City.Should().Be(clinic.City);
        clinicForVerification.ZipCode.Should().Be(clinic.ZipCode);
        
        _clinicRepositoryMock.Verify(repo => repo.Update(It.IsAny<Clinic>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        
        IReadOnlyCollection<IDomainEvent> domainEvents = clinic.GetDomainEvents();
        domainEvents.Should().HaveCount(1);
    }
}
