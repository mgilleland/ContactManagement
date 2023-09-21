namespace ContactManagement.BlazorShared.Models.ContactModels.Update;

public class UpdateContactResponse
{
    public UpdateContactResponse(ContactRecord contact)
    {
        Contact = contact;
    }

    public ContactRecord Contact { get; set; }
}