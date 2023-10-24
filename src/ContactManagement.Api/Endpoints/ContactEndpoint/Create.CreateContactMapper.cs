using ContactManagement.BlazorShared.Models.ContactModels;
using ContactManagement.BlazorShared.Models.ContactModels.Create;
using ContactManagement.Core.Aggregates;
using ContactManagement.Core.ValueObjects;
using FastEndpoints;

namespace ContactManagement.Api.Endpoints.ContactEndpoint;

public class CreateContactMapper : Mapper<CreateContactRequest, CreateContactResponse, Contact>
{
    public override Contact ToEntity(CreateContactRequest request) => new(request.FirstName, request.LastName,
        new AddressType(request.Line1, request.Line2, request.City, request.State, request.Zip), 
        new PhoneNumberType(string.Empty, request.PhoneNumber, request.Extension), 
        request.Age);

    public override CreateContactResponse FromEntity(Contact c) => new (
        new ContactDto
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Line1 = c.Address.Line1,
            Line2 = c.Address.Line2,
            City = c.Address.City,
            State = c.Address.State,
            Zip = c.Address.Zip,
            Number = c.PhoneNumber.PhoneNumber,
            Extension = c.PhoneNumber.Extension,
            Age = c.Age
        });
}