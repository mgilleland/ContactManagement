using ContactManagement.BlazorShared.Models.ContactModels;
using ContactManagement.BlazorShared.Models.ContactModels.Create;
using ContactManagement.BlazorShared.Models.ContactModels.Delete;
using ContactManagement.BlazorShared.Models.ContactModels.GetById;
using ContactManagement.BlazorShared.Models.ContactModels.Update;

namespace ContactManagement.Client.Services
{
    public class ContactService
    {
        private readonly HttpService _httpService;
        private readonly ILogger<ContactService> _logger;

        public ContactService(HttpService httpService, ILogger<ContactService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<ContactDto?> CreateAsync(CreateContactRequest contact)
        {
            return (await _httpService.HttpPostAsync<CreateContactResponse>("contact", contact))?.Contact;
        }

        public async Task<ContactDto?> UpdateAsync(UpdateContactRequest contact)
        {
            return (await _httpService.HttpPutAsync<UpdateContactResponse>($"contact/{contact.ContactId}", contact))?.Contact;
        }

        public async Task DeleteAsync(DeleteContactRequest request)
        {
            await _httpService.HttpDeleteAsync($"contact", request.ContactId);
        }

        public async Task<ContactDto?> GetByIdAsync(int contactId)
        {
            return (await _httpService.HttpGetAsync<GetContactByIdResponse>($"contact/{contactId}"))?.Contact;
        }

        public async Task<List<ContactDto>?> ListAsync()
        {
            _logger.LogInformation("Fetching contacts from API.");

            var contacts = (await _httpService.HttpGetAsync<ListContactResponse>($"contact"))?.Contacts;

            return contacts;
        }
    }
}
