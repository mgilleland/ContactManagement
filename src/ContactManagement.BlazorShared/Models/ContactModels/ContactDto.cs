using ContactManagement.Core.ValueObjects;

namespace ContactManagement.BlazorShared.Models.ContactModels;

public class ContactDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Line1 { get; set; } = string.Empty;
    public string? Line2 { get; set; }
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Zip { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string? Extension { get; set; }


    public AddressType Address => new(Line1, Line2, City, State, Zip);

    public PhoneNumberType PhoneNumber => new("", Number, Extension);
}