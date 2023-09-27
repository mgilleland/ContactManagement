using ContactManagement.BlazorShared.Models.ContactModels;
using ContactManagement.BlazorShared.Models.ContactModels.Create;
using ContactManagement.BlazorShared.Models.ContactModels.Update;
using ContactManagement.Client.Services;
using ContactManagement.Core.ValueObjects;
using Microsoft.AspNetCore.Components;

namespace ContactManagement.Client.Pages.Contact;

public partial class UpsertContact
{
    [Parameter]
    public int? Id { get; set; }

    [Inject]
    ContactService? ContactService { get; set; }

    [Inject]
    public required NavigationManager Navigation { get; set; }

    public ContactDto? Contact { get; set; }

    private string _title = "Add";
    private bool _isContactNotFound;

    protected override async Task OnInitializedAsync()
    {
        if (Id.HasValue)
        {
            _title = "Edit";

            Contact = ((await ContactService?.GetByIdAsync(Id.Value)! ?? null)!);

            if (Contact == null)
            {
                _isContactNotFound = true;
            }
        }
        else
        {
            Contact = new ContactDto();
        }
    }

    protected async Task SaveContact()
    {
        if (Id.HasValue)
        {
            var request = new UpdateContactRequest
            {
                ContactId = Id.Value,
                FirstName = Contact?.FirstName!,
                LastName = Contact?.LastName!,
                Address = new AddressType(Contact?.Line1!, Contact?.Line2, Contact?.City!, Contact?.State!, Contact?.Zip!),
                PhoneNumber = new PhoneNumberType(string.Empty, Contact?.Number!, Contact?.Extension!),
                Age = Contact!.Age
            };

            _ = await ContactService?.UpdateAsync(request)!;
        }
        else
        {
            var request = new CreateContactRequest
            {
                FirstName = Contact?.FirstName!,
                LastName = Contact?.LastName!,
                Address = new AddressType(Contact?.Line1!, Contact?.Line2!, Contact?.City!, Contact?.State!, Contact?.Zip!),
                PhoneNumber = new PhoneNumberType(string.Empty, Contact?.Number!, Contact?.Extension!),
                Age = Contact!.Age
            };

            _ = await ContactService?.CreateAsync(request)!;
        }

        Navigation.NavigateTo("/ContactList");
    }

    public void Cancel()
    {
        Navigation.NavigateTo("/ContactList");
    }
}