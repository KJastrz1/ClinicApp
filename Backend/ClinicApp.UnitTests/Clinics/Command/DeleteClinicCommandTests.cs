using ClinicApp.Application.UseCases.Clinics.Command.DeleteClinic;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Clinics.DomainEvents;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Domain.Shared;
using FluentAssertions;
using Moq;
using Xunit;

namespace ClinicApp.UnitTests.Clinics.Command;

public class DeleteClinicCommandTests
{
    private readonly Mock<IClinicRepository> _clinicRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteClinicCommandHandler _handler;

    public DeleteClinicCommandTests()
    {
        _clinicRepositoryMock = new Mock<IClinicRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new DeleteClinicCommandHandler(
            _clinicRepositoryMock.Object,
            _unitOfWorkMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldDeleteClinic_WhenClinicExists()
    {
        // Arrange
        var clinicId = Guid.NewGuid();
        var command = new DeleteClinicCommand(clinicId);
        var clinic = Clinic.Create(
            ClinicId.Create(clinicId).Value,
            PhoneNumber.Create("123456789").Value,
            Address.Create("123 Main St").Value,
            City.Create("Some City").Value,
            ZipCode.Create("12345").Value
        );

        _clinicRepositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<ClinicId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(clinic);

        // Act
        Result result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        _clinicRepositoryMock.Verify(repo => repo.Remove(clinic), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        
        IReadOnlyCollection<IDomainEvent> domainEvents = clinic.GetDomainEvents();
        domainEvents.Should().ContainSingle(e => e is ClinicDeletedDomainEvent);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenClinicDoesNotExist()
    {
        // Arrange
        var clinicId = Guid.NewGuid();
        var command = new DeleteClinicCommand(clinicId);

        _clinicRepositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<ClinicId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Clinic?)null);

        // Act
        Result result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ClinicErrors.NotFound(ClinicId.Create(clinicId).Value));

        _clinicRepositoryMock.Verify(repo => repo.Remove(It.IsAny<Clinic>()), Times.Never);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
