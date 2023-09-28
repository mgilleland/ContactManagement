using ContactManagement.BlazorShared.Models.ContactModels;
using ContactManagement.Client.Services;
using Microsoft.AspNetCore.Components;

namespace ContactManagement.Client.Pages.Contact;

public partial class ContactList
{
    [Inject]
    ContactService? ContactService { get; set; }

    private List<ContactDto> Contacts = new();

    protected override async Task OnInitializedAsync()
    {
        Contacts = (await ContactService?.ListAsync()!)!;
    }
}