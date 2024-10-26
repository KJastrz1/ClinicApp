using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Infrastructure.Database.Contexts;
using ClinicApp.Infrastructure.Database.Repositories.Read;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Contracts.Clinic.Requests;
using Shared.Contracts.Clinic.Responses;
using Shared.Contracts.Shared;

namespace ClinicApp.IntegrationTests;

public class ClinicReadRepositoryTests : IAsyncLifetime
{
    private readonly ClinicReadRepository _repository;

    private readonly WriteDbContext _writeContext;

    private readonly ReadDbContext _readContext;

    public ClinicReadRepositoryTests()
    {
        DbContextOptions<WriteDbContext> optionsWrite = new DbContextOptionsBuilder<WriteDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        DbContextOptions<ReadDbContext> optionsRead = new DbContextOptionsBuilder<ReadDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _writeContext = new WriteDbContext(optionsWrite);
        _readContext = new ReadDbContext(optionsRead);
        _repository = new ClinicReadRepository(_readContext);
    }

    public async Task InitializeAsync()
    {
        await _writeContext.Database.EnsureDeletedAsync();
        await _writeContext.Database.EnsureCreatedAsync();
    }

    public Task DisposeAsync()
    {
        _writeContext.Dispose();
        _readContext.Dispose();
        return Task.CompletedTask;
    }

    [Theory]
    [MemberData(nameof(ClinicTestDataFactory.GetClinicFilterTestData), MemberType = typeof(ClinicTestDataFactory))]
    public async Task GetByFilterAsync_ShouldReturnExpectedResults(List<Clinic> clinics, ClinicFilter filter, int expectedCount)
    {
        // Arrange
        await _writeContext.Clinics.AddRangeAsync(clinics);

        // Act
        PagedItems<ClinicResponse> result = await _repository.GetByFilterAsync(filter, 1, 10, CancellationToken.None);

        // Assert
        result.Items.Should().HaveCount(expectedCount);
        
        if (expectedCount > 0)
        {
            ClinicResponse clinicForVerification = result.Items.First();
            if (filter.City != null)
            {
                clinicForVerification.City.Should().Be(filter.City);
            }

            if (filter.ZipCode != null)
            {
                clinicForVerification.ZipCode.Should().Be(filter.ZipCode);
            }

            if (filter.Address != null)
            {
                clinicForVerification.Address.Should().Be(filter.Address);
            }

            if (filter.PhoneNumber != null)
            {
                clinicForVerification.PhoneNumber.Should().Be(filter.PhoneNumber);
            }
        }
    }

}
