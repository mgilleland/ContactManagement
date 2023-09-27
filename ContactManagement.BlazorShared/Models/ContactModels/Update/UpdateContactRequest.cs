using ContactManagement.Core.ValueObjects;

namespace ContactManagement.BlazorShared.Models.ContactModels.Update;

public class UpdateContactRequest
{
    public const string Route = "/Contacts/{ContactId:int}";
    public static string BuildRoute(int contactId) => Route.Replace("{ContactId:int}", contactId.ToString());

    public int ContactId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required AddressType Address { get; set; }
    public required PhoneNumberType PhoneNumber { get; set; }
    public int Age { get; set; }

}