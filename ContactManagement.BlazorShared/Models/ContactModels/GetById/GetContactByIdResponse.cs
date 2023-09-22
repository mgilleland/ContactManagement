namespace ContactManagement.BlazorShared.Models.ContactModels.GetById;

public class GetContactByIdResponse
{
    public GetContactByIdResponse(ContactRecord contact)
    {
        Contact = contact;
    }

    public ContactRecord Contact { get; set; }
}