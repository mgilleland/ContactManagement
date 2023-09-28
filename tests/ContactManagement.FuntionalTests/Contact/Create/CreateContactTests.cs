using Ardalis.HttpClientTestExtensions;
using ContactManagement.BlazorShared.Models.ContactModels.Create;
using ContactManagement.Core.ValueObjects;
using FluentAssertions;

namespace ContactManagement.FunctionalTests.Contact.Create;

public class CreateContactTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CreateContactTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Create_returns_created_contact()
    {
        var request = new CreateContactRequest
        {
            FirstName = "First",
            LastName = "Last",
            Address = new AddressType("Line1", "Line2", "City", "ST", "12345"),
            PhoneNumber = new PhoneNumberType(string.Empty, "1234567890", "123"),
            Age = 21
        };

        var content = StringContentHelpers.FromModelAsJson(request);

        var result = await _client.PostAndDeserializeAsync<CreateContactResponse>("/Contacts", content);

        result.Should().NotBeNull();
        result.Contact.Should().NotBeNull();
        result.Contact.FirstName.Should().Be("First");
        result.Contact.LastName.Should().Be("Last");
        result.Contact.Address.Line1.Should().Be("Line1");
        result.Contact.Address.Line2.Should().Be("Line2");
        result.Contact.Address.City.Should().Be("City");
        result.Contact.Address.State.Should().Be("ST");
        result.Contact.Address.Zip.Should().Be("12345");
        result.Contact.PhoneNumber.PhoneNumber.Should().Be("1234567890");
        result.Contact.PhoneNumber.Extension.Should().Be("123");
        result.Contact.Age.Should().Be(21);
    }
}