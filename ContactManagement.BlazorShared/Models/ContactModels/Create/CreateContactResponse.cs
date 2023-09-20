namespace ContactManagement.BlazorShared.Models.ContactModels.Create;

public class CreateContactResponse
{
    public CreateContactResponse(ContactRecord contact)
    {
        Contact = contact;
    }

    public ContactRecord Contact { get; set; }
}