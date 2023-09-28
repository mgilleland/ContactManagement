using ContactManagement.Core.Aggregates;
using ContactManagement.Core.ValueObjects;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace ContactManagement.IntegrationTests.Data;

public class EfRepositoryUpdate : BaseEfRepoTestFixture
{
    [Fact]
    public async Task Contact_updated_after_add()
    {
        var repository = GetRepository();
        var firstName = Guid.NewGuid().ToString();

        var contact = new Contact(firstName, "Last",
            new AddressType("Line1", null, "City", "ST", "12345"),
            new PhoneNumberType("", "1234567890", "123"), 12);

        await repository.AddAsync(contact);

        // detach the item so we get a different instance
        _dbContext.Entry(contact).State = EntityState.Detached;


        // fetch the item and update its title
        var newContact = (await repository.ListAsync())
            .FirstOrDefault(c => c.FirstName == firstName);

        newContact.Should().NotBeNull();
        newContact.Should().NotBeSameAs(contact);

        //Update the new contact
        var newId = newContact?.Id;
        var newFirstName = Guid.NewGuid().ToString();
        newContact?.UpdateNames(newFirstName, "Last");
        await repository.UpdateAsync(newContact!);

        var updatedContact = await repository.GetByIdAsync(newId!.Value, CancellationToken.None);

        updatedContact.Should().NotBeNull();
        contact?.FirstName.Should().NotBeSameAs(updatedContact?.FirstName);
    }
}