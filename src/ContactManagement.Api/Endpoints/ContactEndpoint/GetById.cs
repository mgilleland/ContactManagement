using Ardalis.SharedKernel;
using ContactManagement.BlazorShared.Models.ContactModels.GetById;
using ContactManagement.BlazorShared.Models.ContactModels.Update;
using ContactManagement.Core.Aggregates;
using FastEndpoints;

namespace ContactManagement.Api.Endpoints.ContactEndpoint;

public class GetById : Endpoint<GetContactByIdRequest, GetContactByIdResponse, GetContactByIdMapper>
{
    private readonly IRepository<Contact> _repository;

    public GetById(IRepository<Contact> repository)
    {
        _repository = repository;
    }

    public override void Configure()
    {
        Get(UpdateContactRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            // XML Docs are used by default but are overridden by these properties:
            s.Summary = "Gets a single Contact by ID.";
            s.Description = "Get a single Contact by ID.";
        });
    }

    public override async Task HandleAsync(GetContactByIdRequest request, CancellationToken cancellationToken)
    {
        var contact = await _repository.GetByIdAsync(request.ContactId, cancellationToken);

        if (contact == null)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        Response = Map.FromEntity(contact);
    }
}