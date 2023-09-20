using Ardalis.SharedKernel;

namespace ContactManagement.Core.ValueObjects;

public class AddressType : ValueObject
{
    public string Line1 { get; }
    public string? Line2 { get; }
    public string City { get; }
    public string State { get; }
    public string Zip { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private AddressType() {}  // For EF Only
#pragma warning restore CS8618

    public AddressType(string line1, string? line2, string city, string state, string zip)
    {
        Line1 = line1;
        Line2 = line2;
        City = city;
        State = state;
        Zip = zip;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Line1;
        if (Line2 != null) yield return Line2;
        yield return City;
        yield return State;
        yield return Zip;
    }

    public const int MaxAddressLength = 50;
    public const string StateValidator = @"^[A-Z]{2}$";
    public const string ZipValidator = @"^\d{5}(-\d{4})?$";
}