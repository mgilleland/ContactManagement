using ContactManagement.Core.ValueObjects;

namespace ContactManagement.BlazorShared.Models.ContactModels;

public record ContactRecord(int Id, string FirstName, string LastName, AddressType Address, PhoneNumberType PhoneNumber,
    int Age);
