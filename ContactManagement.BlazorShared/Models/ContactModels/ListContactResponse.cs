namespace ContactManagement.BlazorShared.Models.ContactModels;

public class ListContactResponse
{
    public List<ContactDto> Contacts { get; set; } = new();
}