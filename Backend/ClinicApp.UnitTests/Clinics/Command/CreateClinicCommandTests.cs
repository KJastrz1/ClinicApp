using ClinicApp.Application.Actions.Clinics.Command.CreateClinic;
using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Clinics.DomainEvents;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Domain.Shared;
using FluentAssertions;
using Moq;

namespace ClinicApp.UnitTests.Clinics.Command;

public class CreateClinicCommandTests
{
    private readonly Mock<IClinicRepository> _clinicRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateClinicCommandHandler _handler;
    private readonly CreateClinicCommandValidator _validator;

    public CreateClinicCommandTests()
    {
        _clinicRepositoryMock = new Mock<IClinicRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CreateClinicCommandHandler(
            _clinicRepositoryMock.Object,
            _unitOfWorkMock.Object
        );
        _validator = new CreateClinicCommandValidator();
    }

    [Fact]
    public async Task Handle_ShouldCreateClinic_WhenCommandIsValid()
    {
        // Arrange
        var command = new CreateClinicCommand(
            "123456789",
            "123 Main St",
            "Some City",
            "12345"
        );

        Clinic? clinicForVerification = null;

        _clinicRepositoryMock
            .Setup(repo => repo.Add(It.IsAny<Clinic>()))
            .Callback<Clinic>(c => clinicForVerification = c);

        // Act
        Result<Guid> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        clinicForVerification.Should().NotBeNull();
        clinicForVerification!.PhoneNumber.Value.Should().Be(command.PhoneNumber);
        clinicForVerification.Address.Value.Should().Be(command.Address);
        clinicForVerification.City.Value.Should().Be(command.City);
        clinicForVerification.ZipCode.Value.Should().Be(command.ZipCode);

        result.Value.Should().Be(clinicForVerification.Id.Value);

        _clinicRepositoryMock.Verify(repo => repo.Add(It.IsAny<Clinic>()), Times.Once);

        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        
        IReadOnlyCollection<IDomainEvent> domainEvents = clinicForVerification.GetDomainEvents();
        
        domainEvents.Should().ContainSingle(e => e is ClinicCreatedDomainEvent && ((ClinicCreatedDomainEvent)e).ClinicId == clinicForVerification.Id.Value);
    }
}
