using Ardalis.HttpClientTestExtensions;
using ContactManagement.Api;
using ContactManagement.BlazorShared.Models.ContactModels;
using FluentAssertions;

namespace ContactManagement.FunctionalTests.Contact.List;

public class ContactListTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ContactListTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }


    [Fact]
    public async Task List_returns_seed_contacts()
    {
        var result = await _client.GetAndDeserializeAsync<ListContactResponse>("/Contacts");

        result.Should().NotBeNull();
        result.Contacts.Count.Should().BeGreaterOrEqualTo(2);

        var contact1 = result.Contacts.First(c => c.Id == 1);
        contact1.FirstName.Should().Be(SeedData.Contact1.FirstName);
        contact1.LastName.Should().Be(SeedData.Contact1.LastName);
        contact1.Line1.Should().Be(SeedData.Contact1.Address.Line1);
        contact1.Line2.Should().Be(SeedData.Contact1.Address.Line2);
        contact1.City.Should().Be(SeedData.Contact1.Address.City);
        contact1.State.Should().Be(SeedData.Contact1.Address.State);
        contact1.Zip.Should().Be(SeedData.Contact1.Address.Zip);
        contact1.Number.Should().Be(SeedData.Contact1.PhoneNumber.PhoneNumber);
        contact1.Extension.Should().Be(SeedData.Contact1.PhoneNumber.Extension);
        contact1.Age.Should().Be(SeedData.Contact1.Age);
    }
}