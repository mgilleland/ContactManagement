using Ardalis.HttpClientTestExtensions;
using ContactManagement.Api;
using ContactManagement.BlazorShared.Models.ContactModels.GetById;
using FluentAssertions;

namespace ContactManagement.FunctionalTests.Contact.GetById;

public class ContactGetByIdTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ContactGetByIdTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetById_returns_seed_contact()
    {
        var result = await _client.GetAndDeserializeAsync<GetContactByIdResponse>(GetContactByIdRequest.BuildRoute(1));

        result.Should().NotBeNull();
        result.Contact.Id.Should().Be(1);
        result.Contact.FirstName.Should().Be(SeedData.Contact1.FirstName);
        result.Contact.LastName.Should().Be(SeedData.Contact1.LastName);
        result.Contact.Line1.Should().Be(SeedData.Contact1.Address.Line1);
        result.Contact.Line2.Should().Be(SeedData.Contact1.Address.Line2);
        result.Contact.City.Should().Be(SeedData.Contact1.Address.City);
        result.Contact.State.Should().Be(SeedData.Contact1.Address.State);
        result.Contact.Zip.Should().Be(SeedData.Contact1.Address.Zip);
        result.Contact.Number.Should().Be(SeedData.Contact1.PhoneNumber.PhoneNumber);
        result.Contact.Extension.Should().Be(SeedData.Contact1.PhoneNumber.Extension);
        result.Contact.Age.Should().Be(SeedData.Contact1.Age);
    }

    [Fact]
    public async Task GetById_returns_not_found()
    {
        var route = GetContactByIdRequest.BuildRoute(999);
        _ = await _client.GetAndEnsureNotFoundAsync(route);
    }
}