using ContactManagement.Core.ValueObjects;

namespace ContactManagement.BlazorShared.Models.ContactModels.Create;

public class CreateContactRequest
{
    public const string Route = "/Contacts";

    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public required AddressType Address { get; set; }
    public required string PhoneNumber { get; set; }
    public string? Extension { get; set; }
    public int Age { get; set; }
}