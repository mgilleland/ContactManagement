namespace ContactManagement.BlazorShared.Models.ContactModels.Update;

public class UpdateContactResponse
{
    public UpdateContactResponse(ContactDto contact)
    {
        Contact = contact;
    }

    public ContactDto Contact { get; set; }
}