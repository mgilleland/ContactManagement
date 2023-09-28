using ContactManagement.Core.Aggregates;
using ContactManagement.Core.ValueObjects;
using ContactManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactManagement.Api;

public static class SeedData
{
    public static readonly Contact Contact1 = new Contact("Luke", "Skywalker",
        new AddressType("123 Tatooine St", null, "Middle of Nowhere", "AZ", "12345"),
        new PhoneNumberType("1", "8135551212", null), 19);
    public static readonly Contact Contact2 = new Contact("Obiwan", "Kenobi",
        new AddressType("321 Cave Dr", "Last cave on the left", "Nowhere", "AZ", "12345"),
        new PhoneNumberType("1", "8135552121", null), 57);


    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var dbContext = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

        // Look for any Contacts.
        if (dbContext.Contacts.Any())
        {
            return;   // DB has been seeded
        }

        PopulateTestData(dbContext);
    }

    public static void PopulateTestData(AppDbContext dbContext)
    {
        foreach (var item in dbContext.Contacts)
        {
            dbContext.Remove(item);
        }
        dbContext.SaveChanges();

        dbContext.Contacts.Add(Contact1);
        dbContext.Contacts.Add(Contact2);

        dbContext.SaveChanges();
    }
}