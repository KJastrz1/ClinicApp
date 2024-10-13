using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Clinics.ValueObjects;

public sealed class City : ValueObject
{
    public const int MaxLength = 100;

    private City(string value) => Value = value;

    private City() { }

    public string Value { get; private set; }

    public static Result<City> Create(string city) =>
        Result.Create(city)
            .Ensure(
                c => !string.IsNullOrWhiteSpace(c),
                ClinicErrors.CityErrors.Required)
            .Ensure(
                c => c.Length <= MaxLength,
                ClinicErrors.CityErrors.TooLong)
            .Map(c => new City(c));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
