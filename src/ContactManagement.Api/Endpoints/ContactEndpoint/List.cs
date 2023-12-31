﻿using Ardalis.SharedKernel;
using ContactManagement.BlazorShared.Models.ContactModels;
using ContactManagement.Core.Aggregates;
using FastEndpoints;

namespace ContactManagement.Api.Endpoints.ContactEndpoint;

public class List : EndpointWithoutRequest<ListContactResponse, ContactMapper>
{
    private readonly IRepository<Contact> _repository;

    public List(IRepository<Contact> repository)
    {
        _repository = repository;
    }

    public override void Configure()
    {
        Get("/Contact");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var contacts = await _repository.ListAsync(cancellationToken);

        Response.Contacts = contacts
            .Select(c => Map.FromEntity(c)).ToList();
    }
}