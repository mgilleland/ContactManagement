using Ardalis.SharedKernel;
using ContactManagement.BlazorShared.Models.ContactModels.Update;
using ContactManagement.Core.Aggregates;
using ContactManagement.Core.ValueObjects;
using FastEndpoints;

namespace ContactManagement.Api.Endpoints.ContactEndpoint;

public class Update : Endpoint<UpdateContactRequest, UpdateContactResponse, UpdateContactMapper>
{
    private readonly IRepository<Contact> _repository;

    public Update(IRepository<Contact> repository)
    {
        _repository = repository;
    }


    public override void Configure()
    {
        Put(UpdateContactRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            // XML Docs are used by default but are overridden by these properties:
            s.Summary = "Updates a Contact.";
            s.Description = "Update a Contact.";
        });
    }

    public override async Task HandleAsync(UpdateContactRequest request, CancellationToken cancellationToken)
    {
        var existingContact = await _repository.GetByIdAsync(request.ContactId, cancellationToken);

        if (existingContact == null)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        bool isUpdateRequired = false;

        if (existingContact.FirstName != request.FirstName || existingContact.LastName != request.LastName)
        {
            existingContact.UpdateNames(request.FirstName, request.LastName);
            isUpdateRequired = true;
        }

        if (existingContact.Address != request.Address)
        {
            existingContact.UpdateAddress(request.Address);
            isUpdateRequired = true;
        }

        var requestPhoneNumber = new PhoneNumberType(request.PhoneNumber, request.Extension);

        if (existingContact.PhoneNumber != requestPhoneNumber)
        {
            existingContact.UpdatePhoneNumber(requestPhoneNumber);
            isUpdateRequired = true;
        }

        if (existingContact.Age != request.Age)
        {
            existingContact.UpdateAge(request.Age);
            isUpdateRequired = true;
        }

        if (isUpdateRequired)
        {
            await _repository.UpdateAsync(existingContact, cancellationToken);
        }

        Response = Map.FromEntity(existingContact);
    }
}