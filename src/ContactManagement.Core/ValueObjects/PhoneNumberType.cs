using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using System.Text;

namespace ContactManagement.Core.ValueObjects;

public class PhoneNumberType : ValueObject
{
    private readonly string _countryCode = null!;

    public string CountryCode
    {
        get => _countryCode;
        // ReSharper disable once ValueParameterNotUsed
        private init => _countryCode = "1";  // Country Code is hard coded to 1 for now to enforce US only numbers
    }
    public string PhoneNumber { get; }
    public string? Extension { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    // ReSharper disable once UnusedMember.Global
    public PhoneNumberType() { } // This is for EF Only
#pragma warning restore CS8618 // 

    // Default the country code to USA
    // ReSharper disable once UnusedParameter.Local
    public PhoneNumberType(string countryCode, string phoneNumber, string? extension)
    {
        CountryCode = "1";  // Country Code is hard coded to 1 for now to enforce US only numbers
        PhoneNumber = Guard.Against.NullOrWhiteSpace(phoneNumber, nameof(phoneNumber));
        Extension = extension;
    }

    public override string ToString()
    {
        string formattedNumber = PhoneNumber;

        if (long.TryParse(PhoneNumber, out long number))
        {
            formattedNumber = Convert.ToInt64(number).ToString("###-###-####");
        }

        var phone = new StringBuilder($"+{CountryCode} {formattedNumber}");

        if (Extension != null)
        {
            phone.Append($" Ext. {Extension}");
        }

        return phone.ToString();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CountryCode;
        yield return PhoneNumber;
        if (Extension != null) yield return Extension;
    }

    public const string PhoneNumberValidator = @"^\d{10}$";
    public const string ExtensionValidator = @"^\d{0,6}$";
}