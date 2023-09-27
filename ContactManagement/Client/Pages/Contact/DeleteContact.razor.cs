using ContactManagement.BlazorShared.Models.ContactModels;
using ContactManagement.BlazorShared.Models.ContactModels.Delete;
using ContactManagement.Client.Services;
using Microsoft.AspNetCore.Components;

namespace ContactManagement.Client.Pages.Contact;

public partial class DeleteContact
{
    [Parameter]
    public int Id { get; set; }

    [Inject]
    ContactService? ContactService { get; set; }

    [Inject]
    public required NavigationManager Navigation { get; set; }

    public ContactDto? Contact { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Contact = await ContactService?.GetByIdAsync(Id)!;
    }

    private async Task Delete()
    {
        var request = new DeleteContactRequest
        {
            ContactId = Id
        };

        await ContactService?.DeleteAsync(request)!;

        Navigation.NavigateTo("/ContactList");
    }

    private void Cancel()
    {
        Navigation.NavigateTo("/ContactList");
    }
}