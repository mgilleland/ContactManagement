using ContactManagement.Api.Endpoints.ContactEndpoint;
using ContactManagement.BlazorShared.Models.ContactModels.Update;
using ContactManagement.Core.ValueObjects;

namespace ContactManagement.FunctionalTests.Contact.Update;

public class UpdateContactValidatorTests
{
    private readonly UpdateContactValidator _sut = new();

    [Fact]
    public void Required_values_must_be_provided()
    {
        var request = new UpdateContactRequest
        {
            FirstName = string.Empty,
            LastName = string.Empty,
            Address = new AddressType("Line1", "Line2", "City", "ST", "12345"),
            PhoneNumber = new PhoneNumberType(string.Empty, "1234567890", null)
        };

        var result = _sut.TestValidate(request);

        result.ShouldHaveValidationErrorFor(v => v.FirstName);
        result.ShouldHaveValidationErrorFor(v => v.LastName);
        result.ShouldHaveValidationErrorFor(v => v.Age);
    }

    [Fact]
    public void Maximum_lengths_cannot_be_exceeded()
    {
        var request = new UpdateContactRequest
        {
            FirstName = new string('x', Core.Aggregates.Contact.MaxNameLength + 1),
            LastName = new string('x', Core.Aggregates.Contact.MaxNameLength + 1),
            Address = new AddressType(
                new string('x', AddressType.MaxAddressLength + 1),
                new string('x', AddressType.MaxAddressLength + 1),
                 new string('x', AddressType.MaxAddressLength + 1),
                "ST", "12345"),
            PhoneNumber = new PhoneNumberType(string.Empty, "1234567890", null)
        };

        var result = _sut.TestValidate(request);

        result.ShouldHaveValidationErrorFor(v => v.FirstName);
        result.ShouldHaveValidationErrorFor(v => v.LastName);
        result.ShouldHaveValidationErrorFor(v => v.Address.Line1);
        result.ShouldHaveValidationErrorFor(v => v.Address.Line2);
        result.ShouldHaveValidationErrorFor(v => v.Address.City);
    }

    [Fact]
    public void Formatting_must_be_correct()
    {
        var request = new UpdateContactRequest
        {
            FirstName = "First",
            LastName = "Last",
            Address = new AddressType("Line1", "Line2", "City", "xxx", "xxx"),
            PhoneNumber = new PhoneNumberType(string.Empty, "xxx", "xxx")
        };

        var result = _sut.TestValidate(request);

        result.ShouldHaveValidationErrorFor(v => v.Address.State);
        result.ShouldHaveValidationErrorFor(v => v.Address.Zip);
        result.ShouldHaveValidationErrorFor(v => v.PhoneNumber.PhoneNumber);
        result.ShouldHaveValidationErrorFor(v => v.PhoneNumber.Extension);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(136)]
    public void Age_must_be_in_valid_range(int age)
    {
        var request = new UpdateContactRequest
        {
            FirstName = "First",
            LastName = "Last",
            Address = new AddressType("Line1", "Line2", "City", "ST", "12345"),
            PhoneNumber = new PhoneNumberType(string.Empty, "1234567890", null),
            Age = age
        };

        var result = _sut.TestValidate(request);

        result.ShouldHaveValidationErrorFor(v => v.Age);
    }
}