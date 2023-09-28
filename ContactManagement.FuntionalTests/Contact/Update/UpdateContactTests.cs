using Ardalis.HttpClientTestExtensions;
using ContactManagement.BlazorShared.Models.ContactModels.Create;
using ContactManagement.BlazorShared.Models.ContactModels.Update;
using ContactManagement.Core.ValueObjects;
using FluentAssertions;

namespace ContactManagement.FunctionalTests.Contact.Update;

public class UpdateContactTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UpdateContactTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Update_returns_updated_contact()
    {
        // Add a contact to update
        var createRequest = new CreateContactRequest
        {
            FirstName = "First",
            LastName = "Last",
            Address = new AddressType("Line1", "Line2", "City", "ST", "12345"),
            PhoneNumber = new PhoneNumberType(string.Empty, "1234567890", "123"),
            Age = 21
        };

        var requestContent = StringContentHelpers.FromModelAsJson(createRequest);

        var createResult = await _client.PostAndDeserializeAsync<CreateContactResponse>("/Contacts", requestContent);

        createResult.Should().NotBeNull();
        createResult.Contact.Should().NotBeNull();

        int contactId = createResult.Contact.Id;

        // Build an update request
        var updateRequest = new UpdateContactRequest
        {
            FirstName = "newFirst",
            LastName = "newLast",
            Address = new AddressType("newLine1", "newLine2", "newCity", "XX", "54321"),
            PhoneNumber = new PhoneNumberType(string.Empty, "9876543210", "321"),
            Age = 1
        };

        var route = UpdateContactRequest.BuildRoute(contactId);
        var updateContent = StringContentHelpers.FromModelAsJson(updateRequest);

        var updateResult = await _client.PutAndDeserializeAsync<UpdateContactResponse>(route, updateContent);

        updateResult.Should().NotBeNull();
        updateResult.Contact.Should().NotBeNull();
        updateResult.Contact.FirstName.Should().Be("newFirst");
        updateResult.Contact.LastName.Should().Be("newLast");
        updateResult.Contact.Address.Line1.Should().Be("newLine1");
        updateResult.Contact.Address.Line2.Should().Be("newLine2");
        updateResult.Contact.Address.City.Should().Be("newCity");
        updateResult.Contact.Address.State.Should().Be("XX");
        updateResult.Contact.Address.Zip.Should().Be("54321");
        updateResult.Contact.PhoneNumber.PhoneNumber.Should().Be("9876543210");
        updateResult.Contact.PhoneNumber.Extension.Should().Be("321");
        updateResult.Contact.Age.Should().Be(1);
    }
}