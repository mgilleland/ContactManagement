using ContactManagement.Core.Aggregates;
using ContactManagement.Core.ValueObjects;
using FluentAssertions;

namespace ContactManagement.IntegrationTests.Data;

public class EfRepositoryAdd : BaseEfRepoTestFixture
{
    [Fact]
    public async Task Contact_ID_is_set_on_add()
    {
        var repository = GetRepository();

        var contact = new Contact("First", "Last",
            new AddressType("Line1", null, "City", "ST", "12345"),
            new PhoneNumberType("", "1234567890", "123"), 12);

        await repository.AddAsync(contact);

        var newContact = (await repository.ListAsync()).FirstOrDefault();

        newContact?.Id.Should().BeGreaterThan(0);
        newContact?.FirstName.Should().Be("First");
        newContact?.LastName.Should().Be("Last");
        newContact?.Address.Line1.Should().Be("Line1");
        newContact?.Address.Line2.Should().BeNull();
        newContact?.Address.City.Should().Be("City");
        newContact?.Address.State.Should().Be("ST");
        newContact?.Address.Zip.Should().Be("12345");
        newContact?.PhoneNumber.CountryCode.Should().Be("1");
        newContact?.PhoneNumber.PhoneNumber.Should().Be("1234567890");
        newContact?.PhoneNumber.Extension.Should().Be("123");
        newContact?.Age.Should().Be(12);
    }
}