using Ardalis.SharedKernel;
using ContactManagement.BlazorShared.Models.ContactModels.Create;
using ContactManagement.Core.Aggregates;
using FastEndpoints;

namespace ContactManagement.Api.Endpoints.ContactEndpoint;

public class Create : Endpoint<CreateContactRequest, CreateContactResponse, CreateContactMapper>
{
    private readonly IRepository<Contact> _repository;

    public Create(IRepository<Contact> repository)
    {
        _repository = repository;
    }

    public override void Configure()
    {
        Post(CreateContactRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            // XML Docs are used by default but are overridden by these properties:
            s.Summary = "Creates a new Contact.";
            s.Description = "Create a new Contact.";
        });
    }

    public override async Task HandleAsync(CreateContactRequest request, CancellationToken cancellationToken)
    {
        var contact = Map.ToEntity(request);
        var newContact = await _repository.AddAsync(contact, cancellationToken);
        Response = Map.FromEntity(newContact);
    }
}