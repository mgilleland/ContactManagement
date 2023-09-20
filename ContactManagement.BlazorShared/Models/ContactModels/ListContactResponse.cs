using ContactManagement.Core.Aggregates;

namespace ContactManagement.BlazorShared.Models.ContactModels;

public class ListContactResponse
{
    public List<Contact> Contacts { get; set; } = new();
}