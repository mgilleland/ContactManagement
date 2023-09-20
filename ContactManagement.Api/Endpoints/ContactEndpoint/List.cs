using Ardalis.SharedKernel;
using ContactManagement.BlazorShared.Models.ContactModels;
using ContactManagement.Core.Aggregates;
using FastEndpoints;

namespace ContactManagement.Api.Endpoints.ContactEndpoint;

public class List : EndpointWithoutRequest<ListContactResponse>
{
    private readonly IRepository<Contact> _repository;

    public List(IRepository<Contact> repository)
    {
        _repository = repository;
    }

    public override void Configure()
    {
        Get("/Contacts");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        Response.Contacts = await _repository.ListAsync(cancellationToken);
    }
}