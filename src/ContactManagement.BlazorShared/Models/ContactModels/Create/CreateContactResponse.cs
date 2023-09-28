namespace ContactManagement.BlazorShared.Models.ContactModels.Create;

public class CreateContactResponse
{
    public CreateContactResponse(ContactDto contact)
    {
        Contact = contact;
    }

    public ContactDto Contact { get; set; }
}