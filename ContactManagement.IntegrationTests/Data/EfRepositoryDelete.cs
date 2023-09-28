using ContactManagement.Core.Aggregates;
using ContactManagement.Core.ValueObjects;

namespace ContactManagement.IntegrationTests.Data;

public class EfRepositoryDelete : BaseEfRepoTestFixture
{
    [Fact]
    public async Task Contact_deleted_after_add()
    {
        var repository = GetRepository();
        var firstName = Guid.NewGuid().ToString();

        var contact = new Contact(firstName, "Last",
            new AddressType("Line1", null, "City", "ST", "12345"),
            new PhoneNumberType("", "1234567890", "123"), 12);

        await repository.AddAsync(contact);

        await repository.DeleteAsync(contact);

        Assert.DoesNotContain(await repository.ListAsync(), c => c.FirstName == firstName);
    }
}