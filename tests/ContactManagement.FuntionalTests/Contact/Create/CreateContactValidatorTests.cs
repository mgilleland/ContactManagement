using ContactManagement.Api.Endpoints.ContactEndpoint;
using ContactManagement.BlazorShared.Models.ContactModels.Create;
using ContactManagement.Core.ValueObjects;

namespace ContactManagement.FunctionalTests.Contact.Create;

public class CreateContactValidatorTests
{
    private readonly CreateContactValidator _sut = new();

    [Fact]
    public void Required_values_must_be_provided()
    {
        var request = new CreateContactRequest
        {
            FirstName = string.Empty,
            LastName = string.Empty,
            Line1 = "Line1",
            Line2 = "Line2",
            City = "City",
            State = "ST",
            Zip = "12345",
            PhoneNumber = "1234567890"
        };

        var result = _sut.TestValidate(request);

        result.ShouldHaveValidationErrorFor(v => v.FirstName);
        result.ShouldHaveValidationErrorFor(v => v.LastName);
    }

    [Fact]
    public void Maximum_lengths_cannot_be_exceeded()
    {
        var request = new CreateContactRequest
        {
            FirstName = new string('x', Core.Aggregates.Contact.MaxNameLength + 1),
            LastName = new string('x', Core.Aggregates.Contact.MaxNameLength + 1),
            Line1 = new string('x', AddressType.MaxAddressLength + 1),
            Line2 = new string('x', AddressType.MaxAddressLength + 1),
            City = new string('x', AddressType.MaxAddressLength + 1),
            State = "ST",
            Zip = "12345",
            PhoneNumber = "1234567890"
        };

        var result = _sut.TestValidate(request);

        result.ShouldHaveValidationErrorFor(v => v.FirstName);
        result.ShouldHaveValidationErrorFor(v => v.LastName);
        result.ShouldHaveValidationErrorFor(v => v.Line1);
        result.ShouldHaveValidationErrorFor(v => v.Line2);
        result.ShouldHaveValidationErrorFor(v => v.City);
    }

    [Fact]
    public void Formatting_must_be_correct()
    {
        var request = new CreateContactRequest
        {
            FirstName = "First",
            LastName = "Last",
            Line1 = "Line1",
            Line2 = "Line2",
            City = "City",
            State = "xxx",
            Zip = "xxx",
            PhoneNumber = "xxx",
            Extension = "xxx"
        };

        var result = _sut.TestValidate(request);

        result.ShouldHaveValidationErrorFor(v => v.State);
        result.ShouldHaveValidationErrorFor(v => v.Zip);
        result.ShouldHaveValidationErrorFor(v => v.PhoneNumber);
        result.ShouldHaveValidationErrorFor(v => v.Extension);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(136)]
    public void Age_must_be_in_valid_range(int age)
    {
        var request = new CreateContactRequest
        {
            FirstName = "First",
            LastName = "Last",
            Line1 = "Line1",
            Line2 = "Line2",
            City = "City",
            State = "ST",
            Zip = "12345",
            PhoneNumber = "1234567890",
            Age = age
        };

        var result = _sut.TestValidate(request);

        result.ShouldHaveValidationErrorFor(v => v.Age);
    }
}