using ContactManagement.Core.ValueObjects;
using FluentAssertions;

namespace ContactManagement.UnitTests.Contact;

public class ContactTests
{
    private readonly Core.Aggregates.Contact _validContact = new("First", "Last",
        new AddressType("Line1", "Line2", "City", "ST", "12345"),
        new PhoneNumberType("", "1234567890", "123"), 12);

    [Fact]
    public void Valid_values_generates_expected_results()
    {
        _validContact.FirstName.Should().Be("First");
        _validContact.LastName.Should().Be("Last");
        _validContact.Address.Line1.Should().Be("Line1");
        _validContact.Address.Line2.Should().Be("Line2");
        _validContact.Address.City.Should().Be("City");
        _validContact.Address.State.Should().Be("ST");
        _validContact.Address.Zip.Should().Be("12345");
        _validContact.PhoneNumber.CountryCode.Should().Be("1");
        _validContact.PhoneNumber.PhoneNumber.Should().Be("1234567890");
        _validContact.PhoneNumber.Extension.Should().Be("123");
    }

    [Fact]
    public void Address_string_is_correctly_formatted()
    {
        _validContact.Address.ToString().Should().Be("Line1 Line2 City ST, 12345");

        var contact = new Core.Aggregates.Contact("First", "Last",
            new AddressType("Line1", null, "City", "ST", "12345"),
            new PhoneNumberType("", "1234567890", "123"), 12);

        contact.Address.ToString().Should().Be("Line1 City ST, 12345");
    }

    [Fact]
    public void PhoneNumber_string_is_correctly_formatted()
    {
        _validContact.PhoneNumber.ToString().Should().Be("+1 123-456-7890 Ext. 123");
    }

    [Fact]
    public void UpdateNames_generates_expected_results()
    {
        var contact = new Core.Aggregates.Contact("First", "Last",
            new AddressType("Line1", null, "City", "ST", "12345"),
            new PhoneNumberType("", "1234567890", "123"), 12);

        contact.UpdateNames("First2", "Last2");

        contact.FirstName.Should().Be("First2");
        contact.LastName.Should().Be("Last2");
    }

    [Fact]
    public void UpdateAddress_generates_expected_results()
    {
        var contact = new Core.Aggregates.Contact("First", "Last",
            new AddressType("Line1", null, "City", "ST", "12345"),
            new PhoneNumberType("", "1234567890", "123"), 12);

        var newAddress = new AddressType("New Line1", "New Line2", "New City", "XX", "12345-6789");

        contact.UpdateAddress(newAddress);

        contact.Address.Line1.Should().Be("New Line1");
        contact.Address.Line2.Should().Be("New Line2");
        contact.Address.City.Should().Be("New City");
        contact.Address.State.Should().Be("XX");
        contact.Address.Zip.Should().Be("12345-6789");
    }

    [Fact]
    public void UpdatePhoneNumber_generates_expected_results()
    {
        var contact = new Core.Aggregates.Contact("First", "Last",
            new AddressType("Line1", null, "City", "ST", "12345"),
            new PhoneNumberType("", "1234567890", "123"), 12);

        var newPhone = new PhoneNumberType("", "9876543210", null);

        contact.UpdatePhoneNumber(newPhone);

        contact.PhoneNumber.PhoneNumber.Should().Be("9876543210");
        contact.PhoneNumber.Extension.Should().BeNull();
    }

    [Fact]
    public void FirstName_is_required()
    {
        Action act = () =>
        {
            _ = new Core.Aggregates.Contact("", "", null!, null!, -1);
        };

        act.Should().Throw<ArgumentException>().WithParameterName("firstName");
    }

    [Fact]
    public void LastName_is_required()
    {
        Action act = () =>
        {
            _ = new Core.Aggregates.Contact("First", "", null!, null!, -1); 

        };

        act.Should().Throw<ArgumentException>().WithParameterName("lastName");
    }

    [Fact]
    public void Address_is_required()
    {
        Action act = () =>
        {
            _ = new Core.Aggregates.Contact("First", "Last", null!, null!, -1);
        };

        act.Should().Throw<ArgumentException>().WithParameterName("address");
    }

    [Fact]
    public void Address_Line1_is_required()
    {
        Action act = () =>
        {
            _ = new Core.Aggregates.Contact("First", "Last", 
                new AddressType("", null, "", "", ""), 
                new PhoneNumberType("", "1234567890", "123"), 1);
        };

        act.Should().Throw<ArgumentException>().WithParameterName("line1");
    }

    [Fact]
    public void Address_City_is_required()
    {
        Action act = () =>
        {
            _ = new Core.Aggregates.Contact("First", "Last",
                new AddressType("Line1", null, "", "", ""),
                new PhoneNumberType("", "1234567890", "123"), 1);
        };

        act.Should().Throw<ArgumentException>().WithParameterName("city");
    }

    [Fact]
    public void Address_State_is_required()
    {
        Action act = () =>
        {
            _ = new Core.Aggregates.Contact("First", "Last",
                new AddressType("Line1", null, "City", "", ""),
                new PhoneNumberType("", "1234567890", "123"), 1);
        };

        act.Should().Throw<ArgumentException>().WithParameterName("state");
    }

    [Fact]
    public void Address_Zip_is_required()
    {
        Action act = () =>
        {
            _ = new Core.Aggregates.Contact("First", "Last",
                new AddressType("Line1", null, "City", "ST", ""),
                new PhoneNumberType("", "1234567890", "123"), 1);
        };

        act.Should().Throw<ArgumentException>().WithParameterName("zip");
    }

    [Fact]
    public void PhoneNumber_Number_is_required()
    {
        Action act = () =>
        {
            _ = new Core.Aggregates.Contact("First", "Last",
                new AddressType("Line1", null, "City", "ST", "12345"),
                new PhoneNumberType("", "", ""), 1);
        };

        act.Should().Throw<ArgumentException>().WithParameterName("phoneNumber");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(136)]
    public void PhoneNumber_Extension_is_required(int age)
    {
        Action act = () =>
        {
            _ = new Core.Aggregates.Contact("First", "Last",
                new AddressType("Line1", null, "City", "ST", "12345"),
                new PhoneNumberType("", "1234567890", ""), age);
        };

        act.Should().Throw<ArgumentException>().WithParameterName("age");
    }
}