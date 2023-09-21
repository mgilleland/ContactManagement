using Ardalis.SharedKernel;
using ContactManagement.BlazorShared.Models.ContactModels.Delete;
using ContactManagement.Core.Aggregates;
using FastEndpoints;

namespace ContactManagement.Api.Endpoints.ContactEndpoint;

public class Delete : Endpoint<DeleteContactRequest>
{
    private readonly IRepository<Contact> _repository;

    public Delete(IRepository<Contact> repository)
    {
        _repository = repository;
    }

    public override void Configure()
    {
        Delete(DeleteContactRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            // XML Docs are used by default but are overridden by these properties:
            s.Summary = "Deletes a Contact.";
            s.Description = "Delete a Contact.";
        });
    }

    public override async Task HandleAsync(DeleteContactRequest request, CancellationToken cancellationToken)
    {
        var existingContact = await _repository.GetByIdAsync(request.ContactId, cancellationToken);

        if (existingContact == null)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        await _repository.DeleteAsync(existingContact, cancellationToken);

        await SendNoContentAsync(cancellationToken);
    }
}