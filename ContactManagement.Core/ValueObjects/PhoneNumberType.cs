using Ardalis.SharedKernel;
using System.Text;

namespace ContactManagement.Core.ValueObjects;

public class PhoneNumberType : ValueObject
{
    public string CountryCode { get; }
    public string PhoneNumber { get; }
    public string? Extension { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public PhoneNumberType() { } // This is for EF Only
#pragma warning restore CS8618 // 

    public PhoneNumberType(string countryCode, string phoneNumber, string? extension)
    {
        CountryCode = countryCode;
        PhoneNumber = phoneNumber;
        Extension = extension;
    }

    // Default the country code to USA
    public PhoneNumberType(string phoneNumber, string? extension)
    {
        CountryCode = "1";
        PhoneNumber = phoneNumber;
        Extension = extension;
    }

    public override string ToString()
    {
        var formattedNumber = Convert.ToInt64(PhoneNumber).ToString("###-###-####");
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