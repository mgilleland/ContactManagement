using ContactManagement.Core.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace ContactManagement.BlazorShared.Models.ContactModels.Update;

public class UpdateContactRequest
{
    public const string Route = "/Contacts/{ContactId:int}";
    public static string BuildRoute(int contactId) => Route.Replace("{ContactId:int}", contactId.ToString());

    public int ContactId { get; set; }

    [Required]
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required AddressType Address { get; set; }
    public required string PhoneNumber { get; set; }
    public string? Extension { get; set; }
    public int Age { get; set; }

}