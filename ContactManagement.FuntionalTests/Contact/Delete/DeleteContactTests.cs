using Ardalis.HttpClientTestExtensions;
using ContactManagement.BlazorShared.Models.ContactModels.Create;
using ContactManagement.BlazorShared.Models.ContactModels.Delete;
using ContactManagement.BlazorShared.Models.ContactModels.GetById;
using ContactManagement.Core.ValueObjects;
using FluentAssertions;

namespace ContactManagement.FunctionalTests.Contact.Delete;

public class DeleteContactTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public DeleteContactTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Delete_removes_the_contact()
    {
        // Add a contact to delete
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

        // Delete the contact
        var deleteRoute = DeleteContactRequest.BuildRoute(contactId);

        await _client.DeleteAsync(deleteRoute, CancellationToken.None);

        // Make sure the contact is no longer in the DB
        var getRoute = GetContactByIdRequest.BuildRoute(contactId);
        _ = await _client.GetAndEnsureNotFoundAsync(getRoute);
    }
}