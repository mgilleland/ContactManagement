namespace ContactManagement.BlazorShared.Models.ContactModels.GetById;

public class GetContactByIdResponse
{
    public GetContactByIdResponse(ContactDto contact)
    {
        Contact = contact;
    }

    public ContactDto Contact { get; set; }
}