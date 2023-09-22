using ContactManagement.Core.Aggregates;
using ContactManagement.Core.ValueObjects;
using FastEndpoints;

namespace ContactManagement.BlazorShared.Models.ContactModels.Create;

public class CreateContactMapper : Mapper<CreateContactRequest, CreateContactResponse, Contact>
{
    public override Contact ToEntity(CreateContactRequest request) => new(request.FirstName, request.LastName,
        request.Address, new PhoneNumberType(request.PhoneNumber, request.Extension), request.Age);

    public override CreateContactResponse FromEntity(Contact c) =>
        new(new ContactRecord(c.Id, c.FirstName, c.LastName, c.Address, c.PhoneNumber, c.Age));
}